using KropkaNet.Objects.Dtos.ClientSide.Order;
using KropkaNet.Objects.Dtos.CompanySide.Employee;
using KropkaNet.Objects.Dtos.CompanySide.Warehouse;
using KropkaNet.Objects.Entities.Models.ClientSide;

namespace KropkaNet.Objects.Dtos.CompanySide.Stocktaking
{
    public class StocktakingDetailsDto
    {
        public int Id { get; set; }
        public int ExpectedTimeHours { get; set; }
        public string? Note { get; set; }
        public DateTime DateOfOrderExecution { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public Address Address{ get; set; }
    }
}
