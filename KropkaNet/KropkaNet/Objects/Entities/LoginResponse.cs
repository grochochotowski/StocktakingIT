namespace KropkaNet.Objects.Entities
{
    public class LoginResponse
    {
        public bool IsLoggedIn { get; set; } = false;
        public string Level { get; set; }
        public int PersonId { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
