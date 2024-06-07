using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AutoMapper;
using KropkaNetApi.Exceptions;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Dtos.CompanySide.Product;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities.Models.CompanySide;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Api.Services.CompanySide
{
    public interface IWarehouseProductService
    {
        List<ProductListDto> GetFromWarehouse(int warehouseId);
        void AddProduct(int warehouseId, int productId, int quantity);
        void RemoveProduct(int warehouseId, int productId, int quantity);
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
        public List<ProductListDto> GetFromWarehouse(int warehouseId)
        {
            var products = _context.WarehouseProduct
                .Include(w => w.Product)
                .Where(w => w.WarehouseId == warehouseId)
                .Select(w => new ProductListDto
                {
                    Id = w.Product.Id,
                    Name = w.Product.Name,
                    Category = w.Product.Category,
                    ImgUrl = w.Product.ImgUrl,
                    Quantity = w.Quantity
                })
                .ToList();

            return products;
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
                warehouseProduct = new WarehouseProduct
                {
                    WarehouseId = warehouseId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.WarehouseProduct.Add(warehouseProduct);
            }
            else
            {
                warehouseProduct.Quantity += quantity;
            }

            _context.SaveChanges();
        }

        // PATCH: remove user
        public void RemoveProduct(int warehouseId, int productId, int quantity)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(w => w.Id == warehouseId);
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            var warehouseProduct = _context.WarehouseProduct
                .FirstOrDefault(w => w.WarehouseId == warehouseId && w.ProductId == productId);

            if (warehouse == null) throw new NotFoundException("Warehouse not found");
            if (product == null) throw new NotFoundException("Product not found");
            if (warehouseProduct == null) throw new NotFoundException("Product is not in warehouse");
            if (quantity < 1) throw new BadRequestException("Quantity must be equal or greater than 1");
            if (quantity > warehouseProduct.Quantity) throw new BadRequestException($"Quantity must be equal or less than current quantity {warehouseProduct.Quantity}");

            if (warehouseProduct.Quantity == quantity) _context.WarehouseProduct.Remove(warehouseProduct);
            else warehouseProduct.Quantity -= quantity;

            _context.SaveChanges();
        }
    }
}
