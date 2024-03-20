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

        public string? Note { get; set; }



        public int StocktakingId { get; set; }
        public virtual Stocktaking Stocktaking { get; set; }



        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}