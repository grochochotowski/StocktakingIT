using KropkaNetApi.Dtos.ClientSide.Company;
using KropkaNetApi.Dtos.ClientSide.Order;

namespace KropkaNetApi.Dtos.ClientSide.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }


        public int CompanyId { get; set; }
        public virtual CompanyDto Company { get; set; }



        public virtual ICollection<OrderDto>? Orders { get; set; }
    }
}
