using ExchangeRate.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExchangeRate.Data.Models
{
    public class Currency : BaseModel<int>
    {
        [Required]
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;

        [Required]
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
