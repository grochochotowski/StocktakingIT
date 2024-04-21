using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.ClientSide.User;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;

namespace KropkaNetApi.X_Models.ClientSide.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateOfOrderExecution { get; set; }


        public int DepartmentId { get; set; }
        public virtual DepartmentDto Department { get; set; }


        public int StocktakingId { get; set; }
        public virtual StocktakingDto Stocktaking { get; set; }


        public virtual ICollection<UserDto>? Users { get; set; }
    }
}
