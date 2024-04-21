using KropkaNetApi.Dtos.CompanySide.Stocktaking;
using KropkaNetApi.Dtos.CompanySide.WarehouseProduct;

namespace KropkaNetApi.Dtos.CompanySide.Warehouse
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
