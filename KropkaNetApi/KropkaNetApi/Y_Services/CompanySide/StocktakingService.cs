using AutoMapper;
using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IStocktakingService
    {
        int Create(CreateStocktakingDto dto);
        ReturnResult<StocktakingListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
        StocktakingDto GetDetails(int id);
        void Update(int id, StocktakingDto dto);
        void AddEmployee(int stocktakingId, int employeeId);
        void RemoveEmployee(int stocktakingId, int employeeId);
        int Delete(int id);
    }

    public class StocktakingService : IStocktakingService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public StocktakingService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(CreateStocktakingDto dto)
        {
            var stocktaking = _mapper.Map<Stocktaking>(dto);

            var warehouse = new Warehouse();
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();

            stocktaking.WarehouseId = warehouse.Id;

            _context.Stocktakings.Add(stocktaking);
            _context.SaveChanges();

            return stocktaking.Id;
        }

        public ReturnResult<StocktakingListDto> GetAll(int page, string? filter, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Stocktakings
               .Where(s => (string.IsNullOrEmpty(filter) || (
                      s.Note.Contains(filter.ToLower()) ||
                      s.Id.ToString().Contains(filter))
               ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Stocktaking, object>>>
                {
                    { "id", s => s.Id},
                    { "note", s => s.Note},
                    { "expectedTimeHours", s => s.ExpectedTimeHours},
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new StocktakingListDto
                {
                    Id = p.Id,
                    ExpectedTimeHours = p.ExpectedTimeHours,
                    Note = p.Note
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<StocktakingListDto>(items, totalCount);

            return result;
        }

        public StocktakingDto GetDetails(int id)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == id);
            if (stocktaking == null)
                throw new NotFoundException("Stocktaking not found");

            return _mapper.Map<StocktakingDto>(stocktaking);
        }

        public void Update(int id, StocktakingDto dto)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == id);
            if (stocktaking == null)
                throw new NotFoundException("Stocktaking not found");

            _mapper.Map(dto, stocktaking);
            _context.SaveChanges();
        }

        public void AddEmployee(int stocktakingId, int employeeId)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == stocktakingId);
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (stocktaking == null || employee == null)
                throw new NotFoundException("Stocktaking or Employee not found");

            stocktaking.Employee.Add(employee);
            _context.SaveChanges();
        }

        public void RemoveEmployee(int stocktakingId, int employeeId)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == stocktakingId);
            var employee = stocktaking?.Employee.FirstOrDefault(e => e.Id == employeeId);

            if (stocktaking == null || employee == null)
                throw new NotFoundException("Stocktaking or Employee not found");

            stocktaking.Employee.Remove(employee);
            _context.SaveChanges();
        }

        public int Delete(int id)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == id);
            if (stocktaking == null)
                throw new NotFoundException("Stocktaking not found");

            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == stocktaking.WarehouseId);

            var warehouseProducts = _context.WarehouseProduct.Where(w => w.WarehouseId == warehouse.Id).ToList();

            foreach (var warehouseProduct in warehouseProducts)
            {
                _context.WarehouseProduct.Remove(warehouseProduct);
            }
            _context.Warehouses.Remove(warehouse);
            _context.Stocktakings.Remove(stocktaking);
            _context.SaveChanges();
            return 0;
        }
    }
}
