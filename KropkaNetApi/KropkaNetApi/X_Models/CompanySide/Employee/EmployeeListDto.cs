using KropkaNetApi.X_Models.CompanySide.Position;

namespace KropkaNetApi.X_Models.CompanySide.Employee
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
        public virtual PositionDto? PositionName { get; set; }
    }
}
