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
using OfficeOpenXml;

namespace KropkaNet.Api.Services.CompanySide
{
    public interface IWarehouseProductService
    {
        List<ProductListDto> GetFromWarehouse(int warehouseId);
        void AddProduct(int warehouseId, int productId, int quantity);
        void RemoveProduct(int warehouseId, int productId, int quantity);
        byte[] Export(int warehouseId);
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
    
        // GET: export
        public byte[] Export(int warehouseId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var products = _context.WarehouseProduct
                .Include(wp => wp.Product)
                .Where(wp => wp.WarehouseId == warehouseId)
                .ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Products");

            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Category";
            worksheet.Cells[1, 4].Value = "Quantity";
            worksheet.Cells[1, 5].Value = "Notes";

            for (var i = 0; i < products.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = products[i].Product.Id;
                worksheet.Cells[i + 2, 2].Value = products[i].Product.Name;
                worksheet.Cells[i + 2, 3].Value = products[i].Product.Category;
                worksheet.Cells[i + 2, 4].Value = products[i].Quantity;
                worksheet.Cells[i + 2, 5].Value = products[i].Product.Note;
            }

            return package.GetAsByteArray();
        }
    }
}
