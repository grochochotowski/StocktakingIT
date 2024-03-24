using KropkaNet.Models.Dtos.ClientSide.Company;
using KropkaNet.Models.Dtos.ClientSide.Order;

namespace KropkaNet.Models.Dtos.ClientSide.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<OrderDto>? Orderds { get; set; }
        public virtual ICollection<CompanyDto>? Companies { get; set; }
    }
}
