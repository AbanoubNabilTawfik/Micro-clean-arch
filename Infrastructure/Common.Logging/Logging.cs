using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, loggerConfiguration) =>
        {
            var env = context.HostingEnvironment;
            loggerConfiguration.MinimumLevel.Information()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", env.ApplicationName)
            .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
            .Enrich.WithExceptionDetails()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
            .WriteTo.Console();

            if (context.HostingEnvironment.IsDevelopment())
            {
                loggerConfiguration.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Basket", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
                loggerConfiguration.MinimumLevel.Override("Ordering", LogEventLevel.Debug);

            }

            //TODO :: Configure elastic search configurations

            var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            if(!string.IsNullOrEmpty(elasticUrl)) 
             {
                loggerConfiguration.WriteTo.Elasticsearch(
                    new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion =Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv8,
                        IndexFormat ="NeweEommerce-logs-{0:yyy.MM.dd}",
                        MinimumLogEventLevel = LogEventLevel.Debug

                    });
             }
        };
    }
}
