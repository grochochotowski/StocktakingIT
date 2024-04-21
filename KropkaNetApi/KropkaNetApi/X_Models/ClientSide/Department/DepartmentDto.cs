using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Order;

namespace KropkaNetApi.X_Models.ClientSide.Department
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
