using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Warehouse;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IWarehouseService
    {
        int Create(CreateWarehouseDto dto);
        IEnumerable<WarehouseListDto> GetAll();
        WarehouseDto GetById(int id);
        int Delete(int id);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public WarehouseService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(CreateWarehouseDto dto)
        {
            var warehouse = _mapper.Map<Warehouse>(dto);
            _context.Warehouses.Add(warehouse);
            _context.SaveChanges();
            return warehouse.Id;
        }

        public IEnumerable<WarehouseListDto> GetAll()
        {
            var warehouses = _context.Warehouses.ToList();
            return _mapper.Map<List<WarehouseListDto>>(warehouses);
        }

        public WarehouseDto GetById(int id)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == id);
            return _mapper.Map<WarehouseDto>(warehouse);
        }

        public int Delete(int id)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == id);
            if (warehouse == null) return -1;

            _context.Warehouses.Remove(warehouse);
            _context.SaveChanges();
            return 0;
        }
    }
}
