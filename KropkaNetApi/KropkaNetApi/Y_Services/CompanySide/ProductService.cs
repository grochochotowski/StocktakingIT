using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.CompanySide.Product;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IProductService
    {
        IEnumerable<ProductListDto> GetList(string filter);
    }

    public class ProductService : IProductService
    {
        private StocktakingContext _context;
        private readonly IMapper _mapper;

        public ProductService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // Get product list
        public IEnumerable<ProductListDto> GetList(string filter)
        {
            var productList = _context.Products
                .Where(
                    p => filter == null || (
                    p.Name.ToLower().Contains(filter) ||
                    p.Category.ToLower().Contains(filter) ||
                    p.Note.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter)
                ))
                .OrderBy(p => p.Name)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category
                })
                .ToList();

            return productList;
        }
    }
}
