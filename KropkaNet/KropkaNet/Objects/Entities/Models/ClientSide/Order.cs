using KropkaNet.Objects.Entities.Models.CompanySide;
using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Objects.Entities.Models.ClientSide
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Date of the order execution is required")]
        public DateTime DateOfOrderExecution { get; set; }
        public int State { get; set; }



        [Required(ErrorMessage = "Departament is required")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }



        public int? StocktakingId { get; set; }
        public virtual Stocktaking? Stocktaking { get; set; }



        public virtual ICollection<User>? Users { get; set; }
    }
}
