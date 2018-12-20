using System.Xml.Serialization;

namespace ExchangeRate.DataProcessor.ImportDto
{
    [XmlType("channel")]
    public class ArticlesChannelDto
    {
        [XmlElement("item")]
        public ArticleDto[] ArticleDto { get; set; }
    }
}
