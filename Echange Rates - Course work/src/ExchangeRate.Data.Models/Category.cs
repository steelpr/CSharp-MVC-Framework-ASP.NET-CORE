using ExchangeRate.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeRate.Data.Models
{
    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }

        public string Name { get; set; }

        public virtual ICollection<Article>  Articles { get; set; }
    }
}
