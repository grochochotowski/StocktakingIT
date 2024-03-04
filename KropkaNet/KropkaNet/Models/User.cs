using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Pesel { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }


        public List<int> Orderds { get; set; }
        public List<int> Companies { get; set; }
    }
}
