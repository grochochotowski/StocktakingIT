namespace KropkaNetApi.X_Models.ClientSide.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public DateTime DateOfOrderExecution { get; set; }
        public int DepartmentId { get; set; }
        public int StocktakingId { get; set; }
    }
}
