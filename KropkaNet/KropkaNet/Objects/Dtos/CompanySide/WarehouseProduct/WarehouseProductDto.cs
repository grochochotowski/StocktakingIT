using KropkaNet.Objects.Dtos.CompanySide.Product;
using KropkaNet.Objects.Dtos.CompanySide.Warehouse;

namespace KropkaNet.Objects.Dtos.CompanySide.WarehouseProduct
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
