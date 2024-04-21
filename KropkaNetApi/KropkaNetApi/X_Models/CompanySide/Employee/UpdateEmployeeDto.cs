namespace KropkaNetApi.Dtos.CompanySide.Employee
{
    public class UpdateEmployeeDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PersonalNumber { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public int? PositionId { get; set; }
    }
}
