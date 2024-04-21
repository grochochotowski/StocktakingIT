using KropkaNetApi.Dtos.ClientSide.Department;
using KropkaNetApi.Dtos.ClientSide.User;
using KropkaNetApi.Dtos.CompanySide.Stocktaking;

namespace KropkaNetApi.Dtos.ClientSide.Order
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
