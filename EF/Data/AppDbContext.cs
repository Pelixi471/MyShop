using MyShop.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace MyShop.WebAPI.Data
{
    public class AppDbContext: DbContext
    {
        //Список таблиц:
        public DbSet<Product> Product => Set<Product>();
        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<VisitingPages> VisitingPages => Set<VisitingPages>();
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}