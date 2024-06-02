using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.ClientSide.User;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;

namespace KropkaNetApi.X_Models.ClientSide.Order
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime DateOfOrderExecution { get; set; }
        public int State { get; set; }


        public string DepartmentName { get; set; }


        public int? StocktakingId { get; set; }
    }
}
