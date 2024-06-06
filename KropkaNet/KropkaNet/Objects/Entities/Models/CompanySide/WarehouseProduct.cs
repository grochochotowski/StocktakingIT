using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KropkaNet.Objects.Entities.Models.CompanySide
{
    public class WarehouseProduct
    {
        [Key, Column(Order = 0)]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }



        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "Product ID required")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }



        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
