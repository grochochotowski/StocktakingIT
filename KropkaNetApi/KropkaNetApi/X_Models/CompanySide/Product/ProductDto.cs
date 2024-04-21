using KropkaNetApi.X_Models.CompanySide.WarehouseProduct;

namespace KropkaNetApi.X_Models.CompanySide.Product
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
