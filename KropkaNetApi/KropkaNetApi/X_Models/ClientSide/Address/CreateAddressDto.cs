using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Models.ClientSide.Address
{
    public class CreateAddressDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Premises { get; set; }
    }
}
