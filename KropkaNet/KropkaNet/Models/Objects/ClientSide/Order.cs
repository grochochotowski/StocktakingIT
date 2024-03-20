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



        [Required(ErrorMessage = "Departament is required")]
        public int DepartamentId { get; set; }
        public virtual Department Departments { get; set; }



        [Required(ErrorMessage = "Stocktaking is required")]
        public int StocktakingIT { get; set; }
        public virtual Stocktaking Stocktaking { get; set; }



        public virtual ICollection<User>? Users { get; set; }
    }
}
