using KropkaNet.Objects.Dtos.ClientSide.Company;
using KropkaNet.Objects.Dtos.ClientSide.Order;

namespace KropkaNet.Objects.Dtos.ClientSide.Department
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
