using KropkaNet.Objects.Dtos.CompanySide.WarehouseProduct;

namespace KropkaNet.Objects.Dtos.CompanySide.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }


        public virtual ICollection<WarehouseProductDto> WarehouseProducts { get; set; }
    }
}
