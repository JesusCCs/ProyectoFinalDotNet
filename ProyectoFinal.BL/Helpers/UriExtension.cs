using System;
using System.Web;

namespace ProyectoFinal.BL.Helpers
{
    public static class UriExtension
    {
        public static Uri AddQuery(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[paramName] = paramValue;
            uriBuilder.Query = query.ToString() ?? string.Empty;

            return uriBuilder.Uri;
        }
    }
}