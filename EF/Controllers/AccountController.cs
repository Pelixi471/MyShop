using MyShop.Domain.Exceptions;
using MyShop.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Entites;
using HttpModels;
using Microsoft.AspNetCore.Identity;
using ExcelDataReader.Exceptions;

namespace MyShop.WebAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly IPasswordHasher<RegistrationRequest> _passwordHash;

        public AccountController(AccountService accountService, IPasswordHasher<RegistrationRequest> passwordHash)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _passwordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }

        [HttpPost("register")]
        public async Task<ActionResult<Account>> Register(
            RegistrationRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var newAccount = await _accountService.Register(request.Name, request.Email, request.Password, cancellationToken);
                return newAccount;
            }
            catch (EmailAlreadyExistsException)
            {
                return BadRequest("Учетная запись с указанным адресом электронной почты уже существует");
            }
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<Account>> Authenticate(
    AuthenticationRequest request,
    CancellationToken cancellationToken)
        {
            try
            {
                var existedAccount = await _accountService.Authenticate(
                    request.Email,
                    request.Password,
                    cancellationToken);
                return existedAccount;
            }
            catch (NotFoundException)
            {
                return BadRequest("Учетная запись с указанным адресом электронной почты не существует");
            }
            catch (InvalidPasswordException)
            {
                return BadRequest("Неверный пароль");
            }
        }

    }
}
