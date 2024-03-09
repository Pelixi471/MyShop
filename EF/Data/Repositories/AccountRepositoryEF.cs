using MyShop.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Entites;

namespace MyShop.WebAPI.Data.Repositories
{
    public class AccountRepositoryEF : IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        public AccountRepositoryEF(AppDbContext dbContext) 
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));   
        }

        public async Task Add(Account account, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(account);
            await ((IAccountRepository)_dbContext).Add(account, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(email);
            return _dbContext.Accounts.SingleOrDefaultAsync(it => it.Email == email, cancellationToken);
        }

        public Task<Account?> FindById(int id, CancellationToken cancellationToken)
        {
            return ((IAccountRepository)_dbContext).FindById(id, cancellationToken);
        }

        public Task<Account?> GetById(int id, CancellationToken cancellationToken)
        {
            return _dbContext.Accounts.FirstOrDefaultAsync(it => it.Id == id, cancellationToken);
        }
    }
}
