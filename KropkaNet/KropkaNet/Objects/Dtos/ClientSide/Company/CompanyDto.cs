using KropkaNet.Objects.Dtos.ClientSide.Address;
using KropkaNet.Objects.Dtos.ClientSide.Department;
using KropkaNet.Objects.Dtos.ClientSide.User;

namespace KropkaNet.Objects.Dtos.ClientSide.Company
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
        public virtual ICollection<UserDto>? Users { get; set; }
    }
}
