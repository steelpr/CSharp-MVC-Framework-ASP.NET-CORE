using ExchangeRate.Data.EntityConfiguration;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserArticleEntityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
