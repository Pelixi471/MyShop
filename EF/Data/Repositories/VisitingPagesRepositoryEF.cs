using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Entites;
using MyShop.Domain.RepositoryInterfaces;

namespace MyShop.WebAPI.Data.Repositories
{
    public class VisitingPagesRepositoryEF : IVisitingPagesRepository
    {
        private readonly AppDbContext _dbContext;
        private DbSet<VisitingPages> Visiting => _dbContext.Set<VisitingPages>();

        public VisitingPagesRepositoryEF(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task Add(VisitingPages visitingPages, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(visitingPages, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(VisitingPages visitingPages, CancellationToken cancellationToken)
        {
            _dbContext.Remove(visitingPages);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<VisitingPages?> FindByPath(string path, CancellationToken cancellationToken)
        {
            return Visiting.FirstOrDefaultAsync(e => e.Path == path, cancellationToken);
        }

        public async Task<IReadOnlyCollection<VisitingPages>> GetAll(CancellationToken cancellationToken)
        {
            return await Visiting.ToListAsync(cancellationToken);
        }

        public Task<VisitingPages> GetById(int id, CancellationToken cancellationToken)
        {
            return Visiting.FirstAsync(e => e.Id == id, cancellationToken);
        }

        public async Task Update(VisitingPages visitingPages, CancellationToken cancellationToken)
        {
            _dbContext.Entry(visitingPages).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
