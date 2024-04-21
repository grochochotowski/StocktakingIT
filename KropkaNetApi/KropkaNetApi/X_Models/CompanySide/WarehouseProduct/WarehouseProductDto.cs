using KropkaNetApi.Dtos.CompanySide.Product;
using KropkaNetApi.Dtos.CompanySide.Warehouse;

namespace KropkaNetApi.Dtos.CompanySide.WarehouseProduct
{
    public class WarehouseProductDto
    {
        public int WarehouseId { get; set; }
        public virtual WarehouseDto Warehouse { get; set; }


        public int ProductId { get; set; }
        public virtual ProductDto Product { get; set; }


        public int Quantity { get; set; }
    }
}
