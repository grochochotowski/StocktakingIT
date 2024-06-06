using KropkaNet.Objects.Entities.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Objects.Entities.Models.ClientSide
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }



        [Required]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }



        public virtual ICollection<Order>? Orderds { get; set; }
        public virtual ICollection<Company>? Companies { get; set; }
    }
}
