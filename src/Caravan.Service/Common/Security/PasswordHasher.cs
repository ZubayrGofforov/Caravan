using BCrypt.Net;
using Caravan.Service.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Common.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string passwordHash, string salt) Hash(string password)
        {
            string salt = Guid.NewGuid().ToString();
            string strongpassword = salt + password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(strongpassword);
            return (passwordHash, salt);
        }

        public bool Verify(string password, string salt, string passwordHash)
        {
            string strongpassword = salt + password;
            var result = BCrypt.Net.BCrypt.Verify(passwordHash, strongpassword);
            return result;
        }
    }
}
