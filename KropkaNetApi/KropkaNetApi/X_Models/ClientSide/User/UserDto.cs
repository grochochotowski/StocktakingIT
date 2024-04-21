using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Order;

namespace KropkaNetApi.X_Models.ClientSide.User
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
