using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PersonalNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int PositionId { get; set; }
        public string? Note { get; set; }

        [Required]
        public virtual Position Position { get; set; }
    }
}
