using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Objects.Dtos.ClientSide.User
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
    }
}
