using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IWarehouseProductService
    {
        ReturnResult<ProductListDto> GetFromWarehouse(int warehouseId, int page, string filter, string sortBy, SortDirection sortDireciton);
        void AddProduct(int warehouseId, int productId, int quantity);
        void RemoveProduct(int warehouseId, int productId, int quantity);
        void EditQuantity(int warehouseId, int productId, int quantity);
    }
    public class WarehouseProductService : IWarehouseProductService
    {
        private StocktakingContext _context;
        private readonly IMapper _mapper;

        public WarehouseProductService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // GET: get products from warehouse
        public ReturnResult<ProductListDto> GetFromWarehouse(int warehouseId, int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.WarehouseProduct
                .Include(w => w.Product)
                .Where(w => (string.IsNullOrEmpty(filter) || (
                       w.Product.Id.ToString().Contains(filter) ||
                       w.Product.Category.Contains(filter.ToLower()) ||
                       w.Product.Name.Contains(filter.ToLower()))
                ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<WarehouseProduct, object>>>
                {
                    { "id", c => c.Product.Id},
                    { "Category", c => c.Product.Category},
                    { "Name", c => c.Product.Name}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(w => new ProductListDto
                {
                    Id = w.Product.Id,
                    Category = w.Product.Category,
                    Name = w.Product.Name
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<ProductListDto>(items, totalCount);

            return result;
        }


        // PATCH: add user
        public void AddProduct(int warehouseId, int productId, int quantity)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == warehouseId);
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);

            if (warehouse == null) throw new NotFoundException("Warehouse not found");
            if (product == null) throw new NotFoundException("Product not found");
            if (quantity < 1) throw new BadRequestException("Quantity must be equal or greater than 1");

            var warehouseProduct = _context.WarehouseProduct
                .FirstOrDefault(w => w.WarehouseId == warehouseId && w.ProductId == productId);

            if (warehouseProduct == null)
            {
                var warehouseProductDto = _mapper.Map<WarehouseProduct>(warehouseProduct);
                _context.WarehouseProduct.Add(warehouseProductDto);
            }
            else
            {
                warehouseProduct.Quantity += quantity;
            }

            _context.SaveChanges();
        }
    }
}
