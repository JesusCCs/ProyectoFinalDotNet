namespace ProyectoFinal.Core
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        public int AccessTokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }
    
    public class FrontEnd
    {
        public string Url { get; set; }
    }
    
    public class Server
    {
        public string Url { get; set; }
    }
    
    public class Mobile
    {
        public string Url { get; set; }
    }

    public static class Policy
    {
        public const string GymIsTarget = "GymIsTarget";
        public const string AuthIsTarget = "AuthIsTarget";
        public const string GymIsOwner = "GymIsOwner";
    }
    
    public static class App
    {
        public const string RefreshToken = "RefreshToken";
        public const string RefreshTokenProvider = "RefreshTokenProvider";

        public const int TimeDefaultToken = 24;
    }
    
    public static class AnunciosTipo
    {
        public const string Imagen = "imagen";
        public const string Gif = "gif";
        public const string Video = "video";
    }

    public enum EmailType
    {
        ConfirmEmail,
        ConfirmNewEmail,
        ResetPassword
    }
    
    public enum FileType
    {
        Logo,
        Anuncio
    }
}