using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Entities.Objects.CompanySide
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
