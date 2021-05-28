using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinal.Core.Exceptions
{
    public class AppException : ApplicationException
    {
        public ValidationProblemDetails ValidationProblemDetails { get; }

        protected AppException(IDictionary<string, string[]> errors)
        {
            ValidationProblemDetails = new ValidationProblemDetails(errors)
            {
                Status = (int) HttpStatusCode.BadRequest
            };
        }
    }
}