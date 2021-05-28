using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProyectoFinal.Core.Exceptions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ManageException(context, ex);
            }
        }

        private static async Task ManageException(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            
            // Por defecto, suponemos que es por un problema en la validación
            response.StatusCode = (int) HttpStatusCode.BadRequest;

            ValidationProblemDetails validation = null;
            
            switch (ex)
            {
                // Throws de la aplicación con origen conocido
                case AppException exception:
                    validation = exception.ValidationProblemDetails;
                    break;

                // Errores internos no controlados
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            if (validation != null)
            {
                await response.WriteAsync(JsonSerializer.Serialize(validation));
            }
            else
            {
                await response.WriteAsync(JsonSerializer.Serialize(new {Message = "Internal Server Error."}));
            }
        }
    }
}