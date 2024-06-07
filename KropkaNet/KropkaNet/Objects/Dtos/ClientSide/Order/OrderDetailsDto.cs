namespace KropkaNet.Objects.Dtos.ClientSide.Order
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime DateOfOrderExecution { get; set; }
        public int State { get; set; }


        public string DepartmentName { get; set; }


        public int? StocktakingId { get; set; }

        public int? WarehouseId { get; set; }
    }
}
