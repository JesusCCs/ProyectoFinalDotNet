﻿
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace ProyectoFinal.API.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseSpanishLocalization(this IApplicationBuilder app)
        {
            var es = new CultureInfo("es-ES")
            {
                NumberFormat = {NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "."}
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(es),
                SupportedCultures = new List<CultureInfo>
                {
                    es,
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    es,
                }
            });
            
            return app;
        }
    }
}