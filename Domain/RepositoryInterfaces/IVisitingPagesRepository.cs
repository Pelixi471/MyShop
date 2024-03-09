using MyShop.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.RepositoryInterfaces
{
    public interface IVisitingPagesRepository
    {
        Task<VisitingPages> GetById(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VisitingPages>> GetAll(CancellationToken cancellationToken);
        Task Add(VisitingPages visitingPages, CancellationToken cancellationToken);
        Task Update(VisitingPages visitingPages, CancellationToken cancellationToken);
        Task Delete(VisitingPages visitingPages, CancellationToken cancellationToken);
        Task<VisitingPages?> FindByPath(string path, CancellationToken cancellationToken);
    }
}
