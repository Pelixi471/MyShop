using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Entites;
using MyShop.Domain.RepositoryInterfaces;
using MyShop.Domain.Services.Interfaces;

namespace MyShop.WebAPI.Controllers
{
    [Route("api/metrics")]
    [ApiController]
    public class VisitingPagesController : ControllerBase
    {
        [HttpGet("get")]
        public async Task<ActionResult<List<VisitingPages>>> GetVisitingPages(
        IVisitingPagesService visitingPagesService,
        IVisitingPagesRepository visitingPagesRepository,
        CancellationToken cancellationToken)
        {
            if (visitingPagesService is null)
            {
                throw new ArgumentNullException(nameof(visitingPagesService));
            }

            if (visitingPagesRepository is null)
            {
                throw new ArgumentNullException(nameof(visitingPagesRepository));
            }
            var count = await visitingPagesService.GetVisitingPages(visitingPagesRepository, cancellationToken);
            return count;
        }

    }
}
