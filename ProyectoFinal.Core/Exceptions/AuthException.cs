using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.Core.Exceptions
{
    public class UserCreationException : ApplicationException
    {
        private readonly IEnumerable<IdentityError> _errors;
        
        public UserCreationException(IEnumerable<IdentityError> errors)
        {
            _errors = errors;
        }
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