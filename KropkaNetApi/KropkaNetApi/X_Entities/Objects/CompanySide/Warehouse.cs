using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KropkaNet.Models.Objects.CompanySide
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string? Note { get; set; }



        [Required(ErrorMessage = "Stocktaking ID is required")]
        public int StocktakingId { get; set; }
        public virtual Stocktaking Stocktaking { get; set; }



        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}