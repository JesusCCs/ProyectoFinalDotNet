using System;
using System.Collections.Generic;

namespace ProyectoFinal.Core.Exceptions
{
    public class UpdateFailedException : AppException
    {
        public UpdateFailedException(): base(new Dictionary<string, string[]>
        {
            ["General"] = new []{ "Hubo un problema inesperado al actualizar sus datos." }
        })
        {
        }
    }
}