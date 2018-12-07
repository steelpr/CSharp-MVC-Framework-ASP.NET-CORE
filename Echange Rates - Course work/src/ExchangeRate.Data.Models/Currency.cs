using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExchangeRate.Data.Models
{
    public class Currency
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        [Required]
        [Range(0, int.MaxValue)]
        public int Gold { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public decimal Ratio { get; set; }

        [Required]
        public decimal ReverseRate { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
