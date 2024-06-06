namespace KropkaNet.Objects.Dtos.ClientSide.Company
{
    public class CreateCompanyDto
    {
        public string NIP { get; set; }
        public string KRS { get; set; }
        public string CompanyName { get; set; }
        public string? Note { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Premises { get; set; }
    }
}
