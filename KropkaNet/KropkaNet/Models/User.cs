using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"),
            Display(Name = "User first name")]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Order>? Orderds { get; set; }
    }
}
