using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Stocktaking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExpectedTimeHours { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        public string? Note { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<Employee>? Employee { get; set; }
    }
}
