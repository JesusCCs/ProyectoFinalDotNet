namespace ProyectoFinal.API
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpirationInSeconds { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}