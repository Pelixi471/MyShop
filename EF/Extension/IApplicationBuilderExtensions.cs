using ServerDb.Data;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Domain.Entites;
using Microsoft.AspNetCore.Mvc;
using Domain.RepositoryInterfaces;

namespace ServerDb.Extension
{
    public static class IApplicationBuilderExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", (AppDbContext dbContext, CancellationToken cancellationToken) =>
            {
                return dbContext.Product.ToListAsync(cancellationToken);
            });
            app.MapPost("/api/products/add",async (
                [FromServices] IProductRepository productRepository, [FromBody]Product product, CancellationToken cancellationToken) =>
            {
                await productRepository.Add(product, cancellationToken);
            });
            return app;
        }
    }
}
