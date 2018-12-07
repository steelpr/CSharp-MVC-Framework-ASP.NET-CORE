using ExchangeRate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Data.EntityConfiguration
{
    class UserArticleEntityConfiguration : IEntityTypeConfiguration<ExchangeRateUser>
    {
        public void Configure(EntityTypeBuilder<ExchangeRateUser> builder)
        {
            builder.HasMany(x => x.Articles)
                .WithOne(x => x.User);
        }
    }
}
