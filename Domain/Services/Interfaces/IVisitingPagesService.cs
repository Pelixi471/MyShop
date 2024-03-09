using MyShop.Domain.Entites;
using MyShop.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Domain.Services.Interfaces
{
    public interface IVisitingPagesService
    {
        Task AddOrUpdate(string path, IVisitingPagesRepository visitingPagesRepository, CancellationToken cancellationToken);
        Task<List<VisitingPages>> GetVisitingPages(IVisitingPagesRepository visitingPagesRepository, CancellationToken cancellationToken);
    }
}
