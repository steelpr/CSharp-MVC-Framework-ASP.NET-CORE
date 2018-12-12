using ExchangeRate.Data.Common;
using ExchangeRate.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Services.Contracts
{
    public interface ICurrenciesService
    {
        Task Create();
    }
}
