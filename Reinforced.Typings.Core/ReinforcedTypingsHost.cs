using System;
using Microsoft.Extensions.DependencyInjection;

namespace Reinforced.Typings.Core
{
    public static class ReinforcedTypingsHost
    {
        public static ReinforcedTypingsBuilder CreateDefaultBuilder(string[] args) {
            var builder = new ReinforcedTypingsBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ReinforcedTypingsWorker>();
                });

            if (!(builder is ReinforcedTypingsBuilder reinforcedTypingsBuilder)) {
                throw new Exception("Could not build Reinforced Typings builder.");
            }

            return reinforcedTypingsBuilder;
        }
    }
}
