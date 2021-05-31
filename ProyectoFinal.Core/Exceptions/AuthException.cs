using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinal.Core.Exceptions
{
    public class LoginException : AppException
    {
        public LoginException() : base(new Dictionary<string, string[]>
        {
            ["UserNameOrEmail"] = new[] {"O el usuario o la contraseña son inválidos"},
            ["Password"] = new[] {"O el usuario o la contraseña son inválidos"}
        })
        {
        }
    }

    public class EmailAndUserInUseException : AppException
    {
        public EmailAndUserInUseException() : base(new Dictionary<string, string[]>
        {
            ["UserName"] = new[] {"Este usuario ya está en uso"},
            ["Email"] = new[] {"Este email ya está en uso"}
        })
        {
        }
    }

    public class UserInUseException : AppException
    {
        public UserInUseException() : base(new Dictionary<string, string[]>
        {
            ["UserName"] = new[] {"Este usuario ya está en uso"}
        })
        {
        }
    }

    public class EmailInUseException : AppException
    {
        public EmailInUseException() : base(new Dictionary<string, string[]>
        {
            ["Email"] = new[] {"Este email ya está en uso"}
        })
        {
        }
    }

    public class UserCreationException : AppException
    {
        public UserCreationException(IEnumerable<IdentityError> errors) : base(new Dictionary<string, string[]>
        {
            ["UserName"] = new[] {errors.First().Description}
        })
        {
        }
    }

    public class UserNotFoundException : AppException
    {
        public UserNotFoundException() : base(new Dictionary<string, string[]>
        {
            ["General"] = new[] {"Hubo un error a la hora de gestionar su solicitud"}
        })
        {
        }
    }

    public class ResetPasswordException : AppException
    {
        public ResetPasswordException() : base(new Dictionary<string, string[]>
        {
            ["General"] = new[] {"Hubo un problema a la hora de cambiar su contraseña."}
        })
        {
        }
    }

    public class ChangePasswordException : AppException
    {
        public ChangePasswordException() : base(new Dictionary<string, string[]>
        {
            ["General"] = new[] {"Hubo un problema a la hora de cambiar su contraseña."}
        })
        {
        }
    }
}