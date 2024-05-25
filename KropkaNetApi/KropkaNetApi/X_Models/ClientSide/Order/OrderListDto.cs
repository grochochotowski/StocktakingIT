namespace KropkaNetApi.X_Models.ClientSide.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public DateTime DateOfOrderExecution { get; set; }
        public int State { get; set; }
        public string DepartmentName { get; set; }
    }
}
