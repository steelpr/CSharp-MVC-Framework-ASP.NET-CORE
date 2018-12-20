using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using ExchangeRate.DataProcessor.Services.Contracts;

namespace ExchangeRate.DataProcessor
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly IExportDataService dataProcessorService;
        private readonly ILogger logger;

        private Timer timer;


        public TimedHostedService(ILogger<TimedHostedService> logger, IExportDataService dataProcessorService)
        {
            this.logger = logger;
            this.dataProcessorService = dataProcessorService;
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

            dataProcessorService.ExportCurrency();

            dataProcessorService.ExportHardware();
            dataProcessorService.ExportGame();
            dataProcessorService.ExportIt();
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
