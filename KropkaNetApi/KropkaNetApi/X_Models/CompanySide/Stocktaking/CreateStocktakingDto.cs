namespace KropkaNetApi.X_Models.CompanySide.Stocktaking
{
    public class CreateStocktakingDto
    {
        public int OrderId { get; set; }
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }
    }
}
