using KropkaNet.Objects.Entities.Models.ClientSide;
using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Objects.Entities.Models.CompanySide
{
    public class Stocktaking
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Expected time of stocktaking is required")]
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }


        public int? WarehouseId { get; set; }
        public virtual Warehouse? Warehouse { get; set; }



        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }



        public virtual ICollection<Employee>? Employee { get; set; }
    }
}