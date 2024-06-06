namespace KropkaNet.Objects.Dtos.ClientSide.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Premises { get; set; }
    }
}
