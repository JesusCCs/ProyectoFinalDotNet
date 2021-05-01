﻿namespace ProyectoFinal.API
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        
        public int AccessTokenExpirationInMinutes { get; set; }
        public int RefreshTokenExpirationInDays { get; set; }
    }

    public static class Policy
    {
        public const string GymIsTarget = "GymIsTarget";
        public const string AuthIsTarget = "AuthIsTarget";
    }
    
    public static class App
    {
        public const string RefreshToken = "RefreshToken";
        public const string RefreshTokenProvider = "RefreshTokenProvider";

        public const string TimeDefaultToken = "24 horas";
    }
}