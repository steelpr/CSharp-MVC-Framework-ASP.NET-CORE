using ExchangeRate.Data.Common;
using ExchangeRate.Data.Models;
using ExchangeRate.DataProcessor.ImportDto;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExchangeRate.DataProcessor
{
    public class Deserializer
    {
        private readonly IServiceScopeFactory scopeFactory;

        public Deserializer(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task ImportCurrency(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CurrnecyDto[]), new XmlRootAttribute("ROWSET"));

            var deserializer = (CurrnecyDto[])serializer.Deserialize(new StringReader(xmlString));

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IRepository<Currency>>();

                foreach (var currnecyDto in deserializer)
                {
                    if (!IsValid(currnecyDto))
                    {
                        continue;
                    }
                    
                    var currency = new Currency
                    {
                        Gold = currnecyDto.Gold,
                        Name = currnecyDto.Name,
                        Code = currnecyDto.Code,
                        Ratio = currnecyDto.Ratio,
                        ReverseRate = currnecyDto.ReverseRate,
                        Rate = currnecyDto.Rate,
                        UpdateDate = DateTime.Parse(currnecyDto.UpdateDate).Date
                    };

                    await dbContext.AddAsync(currency);
                    await dbContext.SaveChangesAsync();

                    Thread.Sleep(5000);
                }
            }
        }

        public async Task ImportArticles(string xmlString, string categoryType)
        {
            var serializer = new XmlSerializer(typeof(ArticlesChannelDto[]), new XmlRootAttribute("rss"));

            var deserializer = (ArticlesChannelDto[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var articleDto in deserializer)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IRepository<Article>>();

                    var categoryResposiry = scope.ServiceProvider.GetRequiredService<IRepository<Category>>();
                    var categoryName = categoryResposiry.All().FirstOrDefault(x => x.Name == categoryType);

                    if (categoryName == null)
                    {
                        categoryName = new Category
                        {
                            Name = categoryType
                        };
                    }

                    foreach (var articles in articleDto.ArticleDto)
                    {
                        Article article = new Article
                        {
                            Title = articles.Title,
                            Link = articles.Link,
                            Decsription = ArticleDescription(articles.Decsription),
                            Category = categoryName
                        };

                        await dbContext.AddAsync(article);
                        await dbContext.SaveChangesAsync();

                        Thread.Sleep(1000);

                    }
                }
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }

        private string ArticleDescription(string description)
        {
            string result = string.Empty;
            string pattern = @"<p>(.*)<\/p>";

            RegexOptions options = RegexOptions.Multiline;

            foreach (Match match in Regex.Matches(description, pattern, options))
            {
                result = match.Groups[1]?.Value;
                break;
            }

            return result;
        }
    }
}
