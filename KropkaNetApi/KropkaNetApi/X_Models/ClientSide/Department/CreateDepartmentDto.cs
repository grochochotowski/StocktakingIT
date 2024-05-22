using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Models.ClientSide.Department
{
    public class CreateDepartmentDto
    { 
        public string DepartmentName { get; set; }
        public int CompanyId { get; set; }
    }
}
