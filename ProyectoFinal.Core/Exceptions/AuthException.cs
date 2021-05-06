using System;

namespace ProyectoFinal.Core.Exceptions
{
    public class UserCreationException : ApplicationException
    {
    }

    public class UserNotFoundException : ApplicationException
    {
        public UserNotFoundException() : base("Excepción - El usuario no ha podido encontrarse")
        {
        }
    }

    public class ResetPasswordException : ApplicationException
    {
    }

    public class ChangePasswordException : ApplicationException
    {
    }
}