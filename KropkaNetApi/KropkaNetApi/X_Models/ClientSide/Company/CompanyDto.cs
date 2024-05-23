using KropkaNetApi.X_Models.ClientSide.Address;
using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.ClientSide.User;

namespace KropkaNetApi.X_Models.ClientSide.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string NIP { get; set; }
        public string KRS { get; set; }
        public string CompanyName { get; set; }
        public string? Note { get; set; }


        public int AddressId { get; set; }
        public virtual AddressDto Address { get; set; }


        public virtual ICollection<DepartmentDto>? Departments { get; set; }
        public virtual ICollection<UserDto> Users { get; set; }
    }
}
