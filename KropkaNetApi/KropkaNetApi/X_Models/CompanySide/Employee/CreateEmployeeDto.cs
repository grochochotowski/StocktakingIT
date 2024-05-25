using KropkaNetApi.X_Entities.Objects.Shared;
using KropkaNetApi.X_Models.CompanySide.Position;

namespace KropkaNetApi.X_Models.CompanySide.Employee
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
        public string PositionName  { get; set; }
        public int AccountId { get; set; }

    }
}
