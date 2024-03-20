using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects.CompanySide
{
    public class WarehouseProduct
    {
        [Required(ErrorMessage = "Warehouse ID is required")]
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }



        [Required(ErrorMessage = "Product ID required")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }



        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
