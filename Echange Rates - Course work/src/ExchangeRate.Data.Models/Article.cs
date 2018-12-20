using ExchangeRate.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExchangeRate.Data.Models
{
    public class Article : BaseModel<int>
    {
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Url]
        public string Link { get; set; }

        [Required]
        public string Decsription { get; set; }

        [Required]
        public DateTime DateOfPublish { get; set; } = DateTime.UtcNow;
        
        public string Author { get; set; } = "Admin";

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
