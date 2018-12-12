using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRate.DataProcessor
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger logger;
        private readonly Deserializer deserializer;
        private Timer timer;
        private const int startIndex = 30;
        private const int endIndex = 502;

        public TimedHostedService(ILogger<TimedHostedService> logger, Deserializer deserializer)
        {
            this.logger = logger;
            this.deserializer = deserializer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Background Service is starting.");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            logger.LogInformation("Timed Background Service is working.");

            WebClient wc = new WebClient();

            string xml = wc.DownloadString("http://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?download=xml&search=&lang=EN");
            
            string xmlString = xml.Remove(startIndex, endIndex);
                    
            try
            {
                deserializer.ImportCurreny(xmlString);

                logger.LogInformation("The deserialization was Ok");
            }
            catch (Exception)
            {
                logger.LogError("deserialization stopped");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timed Background Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
