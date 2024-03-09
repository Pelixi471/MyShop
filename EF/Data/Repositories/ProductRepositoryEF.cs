using MyShop.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Entites;

namespace MyShop.WebAPI.Data.Repositories
{
    public class ProductRepositoryEF : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepositoryEF(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }
        public async Task Add(Product product, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task<Product> GetById(int id, CancellationToken cancellationToken)=>
            await _dbContext.Product.FirstAsync(it => it.Id == id, cancellationToken);

        public Task Remove(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Remove(product);
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
