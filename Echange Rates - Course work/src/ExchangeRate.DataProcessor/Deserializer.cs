using AutoMapper;
using ExchangeRate.Data.Common;
using ExchangeRate.Data.Models;
using ExchangeRate.DataProcessor.ImportDto;
using ExchangeRate.Services.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        public async Task ImportCurreny(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CurrnecyDto[]), new XmlRootAttribute("ROWSET"));

            var deserializer = (CurrnecyDto[])serializer.Deserialize(new StringReader(xmlString));

            foreach (var currnecyDto in deserializer)
            {
                if (!IsValid(currnecyDto))
                {
                    continue;
                }

                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IRepository<Currency>>();

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
                }
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResult, true);
        }
    }
}
