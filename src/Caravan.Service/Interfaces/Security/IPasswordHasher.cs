using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Interfaces.Security
{
    public interface IPasswordHasher
    {
        public (string passwordHash, string salt) Hash(string password);
        public bool Verify(string password, string salt, string passwordHash);
    }
}
