using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Entities.Objects.CompanySide
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string? Note { get; set; }



        public int? StocktakingId { get; set; }
        public virtual Stocktaking Stocktaking { get; set; }



        public virtual ICollection<WarehouseProduct> WarehouseProducts { get; set; }
    }
}