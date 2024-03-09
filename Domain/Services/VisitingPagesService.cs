using MyShop.Domain.Entites;
using MyShop.Domain.RepositoryInterfaces;
using MyShop.Domain.Services.Interfaces;


namespace MyShop.Domain.Services
{
    public class VisitingPagesService : IVisitingPagesService
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1);
        public async Task AddOrUpdate(string path, IVisitingPagesRepository visitingPagesRepository, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"\"{nameof(path)}\" не может быть неопределенным или пустым.", nameof(path));
            }

            if (visitingPagesRepository is null)
            {
                throw new ArgumentNullException(nameof(visitingPagesRepository));
            }

            await _semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                var existedVisitingPages = await visitingPagesRepository.FindByPath(path, cancellationToken);
                if (existedVisitingPages == null)
                {
                    var visitingPages = new VisitingPages(path);
                    await visitingPagesRepository.Add(visitingPages, cancellationToken);
                    return;
                }
                existedVisitingPages.NumberOfClicks++;
                await visitingPagesRepository.Update(existedVisitingPages, cancellationToken);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<List<VisitingPages>> GetVisitingPages(IVisitingPagesRepository visitingPagesRepository, CancellationToken cancellationToken)
        {
            if (visitingPagesRepository is null)
            {
                throw new ArgumentNullException(nameof(visitingPagesRepository));
            }

            await _semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                var selection = await visitingPagesRepository.GetAll(cancellationToken);
                return selection.ToList();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
}
