using AutoMapper;
using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;
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

        public ReturnResult<StocktakingListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection)
        {
            var query = _context.Stocktakings.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(s => s.Note.Contains(filter));
            }

            var selector = new Dictionary<string, Expression<Func<Stocktaking, object>>>
            {
                ["id"] = s => s.Id,
                ["note"] = s => s.Note,
                ["expectedTimeHours"] = s => s.ExpectedTimeHours
            };

            if (!string.IsNullOrEmpty(sortBy) && selector.ContainsKey(sortBy))
            {
                var sortExpression = selector[sortBy];
                query = sortDirection == SortDirection.ASC ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
            }

            var list = query.Skip((page - 1) * 10).Take(10).ToList();
            var totalItems = query.Count();
            var items = _mapper.Map<List<StocktakingListDto>>(list);

            return new ReturnResult<StocktakingListDto>(items, totalItems);
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
