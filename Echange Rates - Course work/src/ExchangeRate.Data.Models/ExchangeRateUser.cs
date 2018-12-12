using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ExchangeRate.Data.Models
{
    // Add profile data for application users by adding properties to the ExchangeRateUser class
    public class ExchangeRateUser : IdentityUser
    {
        public ExchangeRateUser()
        {
            this.Articles = new HashSet<Article>();
        }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
