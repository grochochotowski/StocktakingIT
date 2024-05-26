using AutoMapper;
using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.CompanySide.Product;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IProductService
    {
        int Create(CreateProductDto dto);
        ReturnResult<ProductListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDireciton);
        ReturnResult<ProductListDto> GetById(int productId, int page, string filter, string sortBy, SortDirection sortDireciton);
        int Update(int id, CreateProductDto dto);
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
        public ReturnResult<ProductListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Products
                .Where(
                    p => filter == null || (
                    p.Name.ToLower().Contains(filter) ||
                    p.Category.ToLower().Contains(filter) ||
                    p.Note.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter)
                ));


            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    { "id", d => d.Id},
                    { "Name", d => d.Name},
                    { "Category", d => d.Category}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
               .Skip(10 * (page - 1))
               .Take(10)
               .OrderBy(p => p.Name)
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

        //GET : get product by id
        public ReturnResult<ProductListDto> GetById(int productId, int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Products
                .Where(p => p.Id == productId);

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                baseQuery = baseQuery.Where(p =>
                    p.Name.ToLower().Contains(filter) ||
                    p.Category.ToLower().Contains(filter) ||
                    p.Note.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter));
            }


            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    { "id", p => p.Id},
                    { "Name", p => p.Name},
                    { "Category", p => p.Category}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
               .Skip(10 * (page - 1))
               .Take(10)
               .OrderBy(p => p.Name)
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

        //PUT : update product

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
