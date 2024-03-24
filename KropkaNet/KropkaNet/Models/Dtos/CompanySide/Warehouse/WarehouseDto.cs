using KropkaNet.Models.Dtos.CompanySide.Stocktaking;
using KropkaNet.Models.Dtos.CompanySide.WarehouseProduct;

namespace KropkaNet.Models.Dtos.CompanySide.Warehouse
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
