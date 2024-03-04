using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public virtual User? User { get; set; }

    }
}
