using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
