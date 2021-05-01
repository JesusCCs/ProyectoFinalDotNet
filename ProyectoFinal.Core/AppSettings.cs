namespace ProyectoFinal.API
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        public int AccessTokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }

    public class ResetPasswordSettings
    {
        public int ResetPasswordTokenExpirationInMinutes { get; set; }
    }
    
    public class ConfirmEmailSettings
    {
        public int ConfirmEmailTokenExpirationInMinutes { get; set; }
    }
    
    public static class Policy
    {
        public const string GymIsOwner = "GymIsOwner";
    }
    
    public static class App
    {
        public const string RefreshToken = "RefreshToken";
        public const string RefreshTokenProvider = "RefreshTokenProvider";
    }
}