using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects.CompanySide
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }
        public int StocktakingId { get; set; }
        public string? Note { get; set; }

        public virtual Stocktaking Stocktaking { get; set; }
        public virtual Product Product { get; set; }
    }
}