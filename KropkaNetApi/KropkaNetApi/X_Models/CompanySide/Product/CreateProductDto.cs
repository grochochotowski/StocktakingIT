namespace KropkaNetApi.X_Models.CompanySide.Product
{
    public class CreateProductDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string? Note { get; set; }
    }
}
