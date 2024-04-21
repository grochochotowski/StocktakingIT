using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Models.CompanySide.Warehouse;

namespace KropkaNetApi.X_Models.CompanySide.WarehouseProduct
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
