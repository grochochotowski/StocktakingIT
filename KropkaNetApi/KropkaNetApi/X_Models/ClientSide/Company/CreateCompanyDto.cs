using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Models.ClientSide.Company
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
