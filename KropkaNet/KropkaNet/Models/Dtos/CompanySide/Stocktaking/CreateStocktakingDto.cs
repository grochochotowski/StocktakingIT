using KropkaNet.Models.Dtos.CompanySide.Warehouse;

namespace KropkaNet.Models.Dtos.CompanySide.Stocktaking
{
    public class CreateStocktakingDto
    {
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }
        public int WarehouseId { get; set; }
        public int OrderId { get; set; }
    }
}
