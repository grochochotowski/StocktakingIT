namespace KropkaNetApi.X_Entities.Objects.Shared
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire {  get; set; }
    }
}
