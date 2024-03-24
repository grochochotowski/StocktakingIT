using KropkaNet.Models.Dtos.ClientSide.Address;
using KropkaNet.Models.Dtos.ClientSide.Department;
using KropkaNet.Models.Dtos.ClientSide.User;

namespace KropkaNet.Models.Dtos.ClientSide.Company
{
    public class CompanyDto
    {
        public int Id { get; set; }
        
        public int NIP { get; set; }
       
        public int KRS { get; set; }
        
        public string CompanyName { get; set; }
        public string? Note { get; set; }


        public int AddressId { get; set; }
        public virtual AddressDto Address { get; set; }

        public virtual ICollection<DepartmentDto>? Departments { get; set; }
        public virtual ICollection<UserDto> Users { get; set; }
    }
}
