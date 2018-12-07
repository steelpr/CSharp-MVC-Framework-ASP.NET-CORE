using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExchangeRate.Data.Models
{
    public class Article
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        public string Decsription { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime DateOfPublish { get; set; } = DateTime.UtcNow.Date;

        [Required]
        public ExchangeRateUser User { get; set; }
    }
}
