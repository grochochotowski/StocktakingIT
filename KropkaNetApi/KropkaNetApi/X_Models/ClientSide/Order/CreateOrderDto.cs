using KropkaNetApi.X_Models.ClientSide.Department;

namespace KropkaNetApi.X_Models.ClientSide.Order
{
    public class CreateOrderDto
    {
        public DateTime DateOfOrderExecution { get; set; }
        public int State { get; set; } = 0;
        public int DepartmentId { get; set; }
    }
}
