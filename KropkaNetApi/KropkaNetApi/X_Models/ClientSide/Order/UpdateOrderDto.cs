namespace KropkaNetApi.Dtos.ClientSide.Order
{
    public class UpdateOrderDto
    {
        public DateTime? DateOfOrderExecution { get; set; }

        public int? ExpectedTimeHours { get; set; }
    }
}
