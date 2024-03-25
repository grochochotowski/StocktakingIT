namespace KropkaNet.Models.Dtos.ClientSide.Order
{
    public class CreateOrderDto
    {
        public DateTime DateOfOrderExecution { get; set; }
        public int DepartmentId { get; set; }

        public int ExpectedTimeHours { get; set; }
    }
}
