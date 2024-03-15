using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects.CompanySide
{
    public class Stocktaking
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Expected time of stocktaking is required")]
        public int ExpectedTimeHours { get; set; }
        [Required(ErrorMessage = "Warehouse ID is required")]
        public int WarehouseId { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Warehouse>? Warehouse { get; set; }
        public virtual ICollection<Employee>? Employee { get; set; }
    }
}