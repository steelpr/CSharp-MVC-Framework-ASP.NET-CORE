using ExchangeRate.Data.Models;
using ExchangeRate.Services.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ExchangeRate.DataProcessor.ImportDto
{
    [XmlType("ROW")]
    public class CurrnecyDto : IMapFrom<Currency>
    {
        [XmlElement("GOLD")]
        [Required]
        [Range(0, int.MaxValue)]
        public int Gold { get; set; }

        [XmlElement("NAME_")]
        [Required]
        public string Name { get; set; }

        [XmlElement("CODE")]
        [Required]
        public string Code { get; set; }

        [XmlElement("RATIO")]
        [Required]
        public decimal Ratio { get; set; }

        [XmlElement("REVERSERATE")]
        [Required]
        public decimal ReverseRate { get; set; }

        [XmlElement("RATE")]
        [Required]
        public decimal Rate { get; set; }

        [XmlElement("CURR_DATE")]
        [Required]
        public string UpdateDate { get; set; }
    }
}
