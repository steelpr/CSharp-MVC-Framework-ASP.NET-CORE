using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;

namespace Sandbox
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger logger;
        private Timer timer;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            this.logger = logger;
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

            string xml = wc.DownloadString("http://www.bnb.bg/Statistics/StExternalSector/StExchangeRates/StERForeignCurrencies/index.htm?download=xml&search=&lang=BG");
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
