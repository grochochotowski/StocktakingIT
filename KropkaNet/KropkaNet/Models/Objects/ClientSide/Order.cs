using System.ComponentModel.DataAnnotations;
using KropkaNet.Models.Objects.CompanySide;

namespace KropkaNet.Models.Objects.ClientSide
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Date of the order execution is required")]
        public DateTime DateOfOrderExecution { get; set; }

        public virtual ICollection<User>? Users { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Department>? Departments { get; set; }
        public virtual Stocktaking Stocktaking { get; set; }

    }
}
