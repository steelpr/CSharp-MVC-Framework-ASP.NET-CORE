using ExchangeRate.Data.Common;
using ExchangeRate.Data.Models;
using ExchangeRate.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly IRepository<Currency> currency;

        public CurrenciesService(IRepository<Currency> currency)
        {
            this.currency = currency;
        }

        public Task Create()
        {
            var currency = new Currency
            {


            };

            return null;
        }
    }
}
