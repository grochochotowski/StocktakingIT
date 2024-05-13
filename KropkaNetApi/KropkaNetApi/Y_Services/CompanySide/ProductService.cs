using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Product;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IProductService
    {
        int Create(CreateProductDto dto);
        IEnumerable<CompanyListDto> GetList(string filter);
        int Delete(int id);
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



        // POST: create product
        public int Create(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);
            _context.SaveChanges();

            return product.Id;
        }

        // GET: get list of products
        public IEnumerable<CompanyListDto> GetList(string filter)
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
                .Select(p => new CompanyListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category
                })
                .ToList();

            return productList;
        }
    
        // DELETE : delete product with id
        public int Delete(int id)
        {
            var product = _context.Products.FirstOrDefault( p => p.Id == id);
            if (product == null) return -1;

            _context.Remove(product);
            _context.SaveChanges();

            return 0;
        }
    }
}
