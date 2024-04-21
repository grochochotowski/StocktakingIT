using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.CompanySide.Warehouse;

namespace KropkaNetApi.X_Models.CompanySide.Stocktaking
{
    public class StocktakingDto
    {
        public int Id { get; set; }
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }


        public int WarehouseId { get; set; }
        public virtual WarehouseDto Warehouse { get; set; }


        public int OrderId { get; set; }
        public virtual OrderDto Order { get; set; }


        public virtual ICollection<EmployeeDto>? Employee { get; set; }
    }
}
