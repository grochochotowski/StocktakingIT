namespace KropkaNet.Objects.Entities
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireSeconds { get; set; } = 60;
        public string JwtIssuer { get; set; }
    }
}
