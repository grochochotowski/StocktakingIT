namespace KropkaNetApi.X_Models.CompanySide.Employee
{
    public class RegisterEmployeeDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }

        public int PositionId { get; set; }
    }
}
