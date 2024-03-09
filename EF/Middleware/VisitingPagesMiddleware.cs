using MyShop.Domain.RepositoryInterfaces;
using MyShop.Domain.Services.Interfaces;

namespace MyShop.WebAPI.Middleware
{
    public class VisitingPagesMiddleware
    {
        private readonly RequestDelegate _next;
       
        private readonly IVisitingPagesService _visitingPagesService;

        public VisitingPagesMiddleware(
            RequestDelegate next,
            IVisitingPagesService visitingPagesService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _visitingPagesService = visitingPagesService ?? throw new ArgumentNullException(nameof(visitingPagesService));
        }

        public async Task InvokeAsync(
            HttpContext context,
            IVisitingPagesRepository visitingPagesRepository)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (visitingPagesRepository is null)
            {
                throw new ArgumentNullException(nameof(visitingPagesRepository));
            }

            await _visitingPagesService.AddOrUpdate(context.Request.Path, visitingPagesRepository, CancellationToken.None);
            await _next(context);
        }
    }
}
