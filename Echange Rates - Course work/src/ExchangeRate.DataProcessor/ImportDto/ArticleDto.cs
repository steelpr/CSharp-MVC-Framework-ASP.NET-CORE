using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ExchangeRate.DataProcessor.ImportDto
{
    [XmlType("item")]
    public class ArticleDto
    {
        [XmlElement("title")]
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [XmlElement("link")]
        [Url]
        public string Link { get; set; }

        [XmlElement("description")]
        [Required]
        public string Decsription { get; set; }
    }
}
