using KropkaNet.Models.Dtos.CompanySide.Warehouse;
using KropkaNet.Models.Dtos.CompanySide.Product;

namespace KropkaNet.Models.Dtos.CompanySide.WarehouseProduct
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
