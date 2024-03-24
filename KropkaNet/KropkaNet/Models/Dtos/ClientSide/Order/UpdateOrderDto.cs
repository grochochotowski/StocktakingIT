namespace KropkaNet.Models.Dtos.ClientSide.Order
{
    public class UpdateOrderDto
    {
        public DateTime? DateOfOrderExecution { get; set; }
        public int? DepartmentId { get; set; }
        public int? StocktakingId { get; set; }
    }
}
