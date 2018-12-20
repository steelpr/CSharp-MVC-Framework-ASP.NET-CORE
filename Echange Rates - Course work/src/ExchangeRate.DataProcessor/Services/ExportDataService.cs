using ExchangeRate.DataProcessor.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeRate.DataProcessor.Services
{
    public class ExportDataService : IExportDataService
    {
        private const int startIndex = 30;
        private const int endIndex = 502;
        private const string deserializedIsSuccessful = "Deserialized {0} was successful";
        private readonly string deserializationStopped = "deserialization stopped";

        private const string CategoryGame = "Game";
        private const string CategoryIt = "It";
        private const string CategoryHardware = "Hardware";

        private readonly WebClient web;
        private readonly Deserializer deserializer;
        private readonly ILogger logger;
               
        public ExportDataService(Deserializer deserializer,
            ILogger<TimedHostedService> logger)
        {
            this.web = new WebClient ();
            this.deserializer = deserializer;
            this.logger = logger;

        }

        public async Task ExportCurrency()
        {
            string xmlCurrency = web.DownloadString("http://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?download=xml&search=&lang=EN");

            string xmlString = xmlCurrency.Remove(startIndex, endIndex);

            try
            {
                await deserializer.ImportCurrency(xmlString);
            }
            catch (Exception)
            {
                logger.LogError(deserializationStopped);
            }
        }

        public async Task ExportGame()
        {
            string xmlGame = web.DownloadString("https://www.kaldata.com/%D0%B8%D0%B3%D1%80%D0%B8/feed");

            try
            {
                await deserializer.ImportArticles(xmlGame, CategoryGame);
            }
            catch (Exception)
            {
                logger.LogError(deserializationStopped);
            }
        }

        public async Task ExportHardware()
        {
            string xmlHardware = web.DownloadString("https://www.kaldata.com/%D1%85%D0%B0%D1%80%D0%B4%D1%83%D0%B5%D1%80/feed");

            try
            {
                await deserializer.ImportArticles(xmlHardware, CategoryHardware);
            }
            catch (Exception)
            {
                logger.LogError(deserializationStopped);
            }
        }

        public async Task ExportIt()
        {
            string xmlIt = web.DownloadString("https://www.kaldata.com/it-%D0%BD%D0%BE%D0%B2%D0%B8%D0%BD%D0%B8/feed");

            try
            {
                await deserializer.ImportArticles(xmlIt, CategoryIt);
            }
            catch (Exception)
            {
                logger.LogError(deserializationStopped);
            }
        }
    }
}
