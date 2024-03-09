using MyShop.Domain.Entites;

namespace MyShop.Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id, CancellationToken cancellationToken);
        Task Add(Product product, CancellationToken cancellationToken);
        Task Update(Product product, CancellationToken cancellationToken);
        Task Remove(Product product, CancellationToken cancellationToken);
    }
}
