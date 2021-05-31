using System.Collections.Generic;

namespace ProyectoFinal.Core.Exceptions
{
    public class TimeVideoExceeded : AppException
    {
        public TimeVideoExceeded(): base(new Dictionary<string, string[]>
        {
            ["Recurso"] = new []{ "El vídeo que ha subido supera los 30 segundos de duración. Escoja otro, por favor." }
        })
        {
        }
    }
}