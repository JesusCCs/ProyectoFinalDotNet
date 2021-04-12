using System.Collections.Generic;

namespace ProyectoFinal.DAL.Models
{
    public class Password
    {
        public string PasswordHash { get; set; }

        private sealed class PasswordHashEqualityComparer : IEqualityComparer<Password>
        {
            public bool Equals(Password x, Password y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.PasswordHash == y.PasswordHash;
            }

            public int GetHashCode(Password obj)
            {
                return (obj.PasswordHash != null ? obj.PasswordHash.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<Password> PasswordHashComparer { get; } = new PasswordHashEqualityComparer();
    }
}