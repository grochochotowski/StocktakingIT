using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfOrderExecution { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        //public virtual ICollection<>  { get; set; }

    }
}
