using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reinforced.Typings.Core;
using Reinforced.Typings.Core.Settings;
using Reinforced.Typings.Fluent;

namespace Reinforced.Typings.Template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            ReinforcedTypingsHost.CreateDefaultBuilder(args)
                .ConfigureReinforcedTypings((builder, services) => {
                    builder.Global(config => {
                        config.UseModules();
                        config.CamelCaseForProperties();
                        config.AutoOptionalProperties();
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddOptions();

                    services.Configure<ReinforcedTypingsSettings>(
                        hostContext.Configuration.GetSection(nameof(ReinforcedTypingsSettings)));
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    var configSection = hostContext.Configuration.GetSection("Logging");

                    logging.AddConfiguration(configSection);
                    logging.AddConsole();
                });
    }
}
