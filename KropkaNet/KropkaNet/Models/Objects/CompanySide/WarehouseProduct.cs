namespace KropkaNet.Models.Objects.CompanySide
{
    public class WarehouseProduct
    {
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
