using KropkaNetApi.Dtos.ClientSide.Order;
using KropkaNetApi.Dtos.CompanySide.Employee;
using KropkaNetApi.Dtos.CompanySide.Warehouse;

namespace KropkaNetApi.Dtos.CompanySide.Stocktaking
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
