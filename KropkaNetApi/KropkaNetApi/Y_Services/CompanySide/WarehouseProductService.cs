using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;
using KropkaNetApi.X_Entities.Objects.CompanySide;

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
    }
}
