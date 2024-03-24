using KropkaNet.Models.Dtos.CompanySide.Employee;
using KropkaNet.Models.Dtos.ClientSide.Order;
using KropkaNet.Models.Dtos.CompanySide.Warehouse;

namespace KropkaNet.Models.Dtos.CompanySide.Stocktaking
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
