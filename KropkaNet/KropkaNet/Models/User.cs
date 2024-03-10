using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Perosnal number is required")]
        public string PersonalNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Order>? Orderds { get; set; }
    }
}
