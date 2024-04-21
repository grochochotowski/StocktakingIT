using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using KropkaNetApi.X_Models.CompanySide.WarehouseProduct;

namespace KropkaNetApi.X_Models.CompanySide.Warehouse
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string? Note { get; set; }


        public int StocktakingId { get; set; }
        public virtual StocktakingDto Stocktaking { get; set; }


        public virtual ICollection<WarehouseProductDto> WarehouseProducts { get; set; }
    }
}
