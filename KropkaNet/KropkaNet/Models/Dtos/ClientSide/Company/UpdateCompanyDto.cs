namespace KropkaNet.Models.Dtos.ClientSide.Company
{
    public class UpdateCompanyDto
    {
        public int? NIP { get; set; }

        public int? KRS { get; set; }

        public string? CompanyName { get; set; }
        public string? Note { get; set; }

        public int? AddressId { get; set; }
    }
}
