using MyShop.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
//using ServerDb.Extension;
using MyShop.WebAPI.Data.Repositories;
using MyShop.Domain.RepositoryInterfaces;
using MyShop.Domain.Services;
using FrontendBlazor;
using MyShop.Domain.Services.Interfaces;
using MyShop.AspNetCorePasswordHasherLib;
using MyShop.Domain.Entites;
using MyShop.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
});

builder.Services.AddScoped<IProductRepository, ProductRepositoryEF>();
builder.Services.AddSingleton<IAppPasswordHasher, AspNetCorePasswordHasher>();
builder.Services.AddScoped<IAccountRepository, AccountRepositoryEF>();
builder.Services.AddScoped<IVisitingPagesRepository, VisitingPagesRepositoryEF>();
builder.Services.AddSingleton<IVisitingPagesService, VisitingPagesService>();
builder.Services.AddScoped<AccountService>();



builder.Services.AddCors();
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
});


app.UseMiddleware<OnlyEdgeMiddleware>();
app.UseMiddleware<VisitingPagesMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
