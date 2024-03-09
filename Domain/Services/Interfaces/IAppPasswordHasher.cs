using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.Services.Interfaces
{
    public interface IAppPasswordHasher
    {
        string HashPassword(string password);

        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
