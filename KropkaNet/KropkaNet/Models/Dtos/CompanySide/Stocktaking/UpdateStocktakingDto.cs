namespace KropkaNet.Models.Dtos.CompanySide.Stocktaking
{
    public class UpdateStocktakingDto
    {
        public int? ExpectedTimeHours { get; set; }
        public string? Note { get; set; }
        public int? WarehouseId { get; set; }
        public int? OrderId { get; set; }
    }
}
