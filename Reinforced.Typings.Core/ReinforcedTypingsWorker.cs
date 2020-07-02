using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Reinforced.Typings.Core
{
    public class ReinforcedTypingsWorker : BackgroundService
    {
        private readonly ILogger<ReinforcedTypingsWorker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly TsExporter _exporter;

        public ReinforcedTypingsWorker(
            IHostApplicationLifetime hostApplicationLifetime,
            ILogger<ReinforcedTypingsWorker> logger,
            TsExporter exporter
        ) {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _exporter = exporter;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Exporting Types: {time}", DateTimeOffset.Now);

            _exporter.Export();

            _logger.LogInformation("Export Completed: {time}", DateTimeOffset.Now);

            _hostApplicationLifetime.StopApplication();

            return Task.CompletedTask;
        }
    }
}
