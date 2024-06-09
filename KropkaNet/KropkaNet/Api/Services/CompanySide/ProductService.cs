using AutoMapper;
using KropkaNet.Objects.Dtos.CompanySide.Product;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities.Models.CompanySide;
using KropkaNetApi.Exceptions;
using System.Linq.Expressions;
using static NuGet.Packaging.PackagingConstants;

namespace KropkaNet.Api.Services.CompanySide
{
    public interface IProductService
    {
        int Create(CreateProductDto dto);
        ReturnResult<ProductListDto> GetAll(int page, string filters, string sortBy, SortDirection sortDirsortDirectioneciton);
        List<ProductListDto> GetNoPag(string filter, string? sortBy, SortDirection sortDirection);
        ProductDto GetById(int productId);
        int Update(int id, CreateProductDto dto);
        void Delete(int productId);
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
        public ReturnResult<ProductListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Products
            .Where(p => (string.IsNullOrEmpty(filter) || (
                    p.Name.ToLower().Contains(filter) ||
                    p.Category.ToLower().Contains(filter) ||
                    p.Note.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter))
            ));


            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    { "id", d => d.Id},
                    { "name", d => d.Name},
                    { "category", d => d.Category}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<ProductListDto>(items, totalCount);

            return result;
        }

        // GET: get list of products
        public List<ProductListDto> GetNoPag(string filter, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Products
            .Where(p => (string.IsNullOrEmpty(filter) || (
                    p.Name.ToLower().Contains(filter) ||
                    p.Category.ToLower().Contains(filter) ||
                    p.Note.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter))
            ));


            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    { "id", d => d.Id},
                    { "name", d => d.Name},
                    { "category", d => d.Category}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var products = baseQuery
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category
                })
                .ToList();

            return products;
        }

        // GET : get product by id
        public ProductDto GetById(int productId)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == productId);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        // PUT : update product
        public int Update(int id, CreateProductDto dto)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Id == id);

            if (product == null) throw new NotFoundException("Product not found");

            product.Category = dto.Category;
            product.Name = dto.Name;
            product.Note = dto.Note;

            _context.SaveChanges();

            return product.Id;
        }

        // DELETE : delete product with id
        public void Delete(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) throw new NotFoundException("Product not found");

            _context.Remove(product);
            _context.SaveChanges();
        }
    }
}
