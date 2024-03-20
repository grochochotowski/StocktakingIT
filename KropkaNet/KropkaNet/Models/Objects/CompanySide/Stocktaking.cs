using KropkaNet.Models.Objects.ClientSide;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KropkaNet.Models.Objects.CompanySide
{
    public class Stocktaking
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Expected time of stocktaking is required")]
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }



        [Required(ErrorMessage = "Warehouse ID is required")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }



        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }



        public virtual ICollection<Employee>? Employee { get; set; }
    }
}