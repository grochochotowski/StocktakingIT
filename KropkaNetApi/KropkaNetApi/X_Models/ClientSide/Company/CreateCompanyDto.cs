using KropkaNet.Models.Dtos.ClientSide.Address;
using KropkaNet.Models.Dtos.ClientSide.Department;
using KropkaNet.Models.Dtos.ClientSide.User;

namespace KropkaNetApi.Dtos.ClientSide.Company
{
    public class CreateCompanyDto
    {
        public int NIP { get; set; }

        public int KRS { get; set; }

        public string CompanyName { get; set; }
        public string? Note { get; set; }

        public int AddressId { get; set; }
    }
}
