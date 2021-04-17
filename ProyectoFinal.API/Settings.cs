namespace ProyectoFinal.API
{
    /// <summary>
    /// Clase Wrapper para la configuración de appsettings sobre los tokens tipo jwt
    /// </summary>
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Secret { get; set; }

        public int ExpirationInDays { get; set; }
    }
}