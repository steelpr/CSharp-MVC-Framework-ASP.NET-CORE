using ExchangeRate.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRate.Data
{
    public class ExchangeRateContext : IdentityDbContext<ExchangeRateUser>
    {
        public ExchangeRateContext(DbContextOptions<ExchangeRateContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
