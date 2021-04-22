using System;

namespace ProyectoFinal.BL.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Excepción - El usuario no ha podido encontrarse")
        {

        }
    }
}