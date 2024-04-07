namespace KropkaNet.Models.Dtos.CompanySide.Warehouse
{
    public class CreateWarehouseDto
    {
        public string? Note { get; set; }
        public int StocktakingId { get; set; }

        public CreateWarehouseDto(string? Note, int StockatkingId)
        {
            this.Note = Note;
            this.StocktakingId = StockatkingId;
        }
    }
}
