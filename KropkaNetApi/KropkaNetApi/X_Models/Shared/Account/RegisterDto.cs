namespace KropkaNetApi.X_Models.Shared.Account
{
    public class RegisterDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
