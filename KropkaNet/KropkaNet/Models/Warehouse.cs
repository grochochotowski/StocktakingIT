using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
