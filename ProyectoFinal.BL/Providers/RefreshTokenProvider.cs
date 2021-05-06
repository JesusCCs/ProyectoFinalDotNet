using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProyectoFinal.DAL.Models.Auth;

namespace ProyectoFinal.BL.Providers
{
    public class RefreshTokenProvider<TAuth>: DataProtectorTokenProvider<TAuth> where TAuth : Auth
    {
        public RefreshTokenProvider(IDataProtectionProvider dataProtectionProvider, 
            IOptions<RefreshTokenProviderOptions> options, ILogger<RefreshTokenProvider<TAuth>> logger) : base(dataProtectionProvider, options, logger)
        {
            
        }
    }
    
    public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        
    }
}