using System.Collections.Generic;

namespace ProyectoFinal.Core.Exceptions
{
    public class ConfirmEmailException : AppException
    {
        public ConfirmEmailException(): base(new Dictionary<string, string[]>
        {
            ["General"] = new []{ "Hubo un problema al confirmar su email" }
        })
        {
        }
    }
}