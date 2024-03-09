using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyShop.Domain.Entites;
using MyShop.Domain.Services.Interfaces;


namespace MyShop.AspNetCorePasswordHasherLib
{
    public class AspNetCorePasswordHasher : IAppPasswordHasher
    {
        private readonly PasswordHasher<Account> _passwordHasher;
        public AspNetCorePasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor)
        {
            _passwordHasher = new PasswordHasher<Account>();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null!, password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
            if(result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                //автоматическое обновление passwordHasher в БД
            }
            return result != PasswordVerificationResult.Failed;
        }
    }
}