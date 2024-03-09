using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Note { get; set; }
    }
}
