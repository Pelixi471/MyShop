using MyShop.Domain.Exceptions;
using MyShop.Domain.RepositoryInterfaces;
using MyShop.Domain.Entites;
using MyShop.Domain.Services.Interfaces;
using ExcelDataReader.Exceptions;

namespace MyShop.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IAppPasswordHasher _passwordHasher;
        public AccountService(IAccountRepository accountRepo, IAppPasswordHasher passwordHasher)
        {
            _accountRepo = accountRepo ?? throw new ArgumentNullException(nameof(accountRepo));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

        }

        public async Task<Account> Register(
            string name,
            string email,
            string password,
            CancellationToken cancellationToken)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            Account? existedAccount = await _accountRepo.FindByEmail(email, cancellationToken);
            if (existedAccount != null)
            {
                throw new EmailAlreadyExistsException($"Предоставленный email {email}, уже зарегистрированные другим пользователем"); ; ; ;
            }
            string passwordHash = _passwordHasher.HashPassword(password);
            var newAccount = new Account(name, email, passwordHash)
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash
            };

            await _accountRepo.Add(newAccount, cancellationToken);
            return newAccount;
        }

        public async Task<Account> Authenticate(
    string email,
    string password,
    CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException($"{nameof(email)} не может быть неопределенным или пустым.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException($"{nameof(password)} не может быть неопределенным или пустым.", nameof(password));
            }

            var existedAccount =
                await _accountRepo.FindByEmail(email, cancellationToken);
            if (existedAccount == null)
                throw new NotFoundException("Учетная запись не найдена");
            var result = _passwordHasher.VerifyHashedPassword(existedAccount.PasswordHash, password);
            if (result == false)
                throw new InvalidPasswordException("Неверный пароль");
            return existedAccount;
        }
    }
}
