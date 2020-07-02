
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reinforced.Typings.Core.Settings;
using Reinforced.Typings.Fluent;

namespace Reinforced.Typings.Core
{
    public class ReinforcedTypingsBuilder : IHostBuilder
    {
        private readonly IHostBuilder _hostBuilder;

        private List<Action<Fluent.ConfigurationBuilder, IServiceProvider>> _configureReinforcedTypingsActions =
            new List<Action<Fluent.ConfigurationBuilder, IServiceProvider>>();

        public ReinforcedTypingsBuilder(string[] args)
        {
            _hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton((serviceProvider) => {
                        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                        var settings = serviceProvider.GetService<ReinforcedTypingsSettings>() ?? new ReinforcedTypingsSettings
                        {
                            Hierarchy = false,
                            TargetDirectory = null,
                            TargetFile = "types/types.ts"
                        };

                        return new ExportContext(assemblies)
                        {
                            Hierarchical = settings.Hierarchy,
                            TargetDirectory = settings.TargetDirectory,
                            TargetFile = settings.TargetFile,
                            DocumentationFilePath = settings.DocumentationFilePath,
                            ConfigurationMethod = (builder) => {
                                foreach (var configureReinforcedTypingsAction in _configureReinforcedTypingsActions)
                                {
                                    configureReinforcedTypingsAction(builder, serviceProvider);
                                }
                            }
                        };
                    });

                    services.AddSingleton<TsExporter>();
                });
        }

        public IDictionary<object, object> Properties => _hostBuilder.Properties;

        // This was a bit lazy
        public IHost Build() => _hostBuilder.Build();

        public IHostBuilder ConfigureAppConfiguration(
            Action<HostBuilderContext, IConfigurationBuilder> configureDelegate
        ) {
            _hostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(
            Action<HostBuilderContext, TContainerBuilder> configureDelegate
        ) {
            _hostBuilder.ConfigureContainer(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(
            Action<IConfigurationBuilder> configureDelegate
        ) {
            _hostBuilder.ConfigureHostConfiguration(configureDelegate);
            return this;
        }

        /// <summary>
        /// Configure Reinforced Typings Fluently
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring Reinforced Typings' <see cref="ConfigurationBuilder"/></param>
        /// <returns>The same instance of <see cref="ReinforcedTypingsBuilder"/> for chaining.</returns>
        public IHostBuilder ConfigureReinforcedTypings(Action<Fluent.ConfigurationBuilder, IServiceProvider> configureDelegate)
        {
            _configureReinforcedTypingsActions.Add(configureDelegate ?? throw new ArgumentNullException(nameof(configureDelegate)));
            return this;
        }

        public IHostBuilder ConfigureServices(
            Action<HostBuilderContext, IServiceCollection> configureDelegate
        ) {
            _hostBuilder.ConfigureServices(configureDelegate);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
            IServiceProviderFactory<TContainerBuilder> factory
        ) {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
            Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory
        ) {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }
    }
}
