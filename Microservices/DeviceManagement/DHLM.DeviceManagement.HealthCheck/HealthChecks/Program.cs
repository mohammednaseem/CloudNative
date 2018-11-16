using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HealthChecksSample
{
    public class Program
    {
        static Program()
        {
         
        }

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .AddCommandLine(args)
                .Build();
 

            return new WebHostBuilder()
                .UseConfiguration(config)
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddConfiguration(config);
                    builder.AddConsole();
                })
                .UseKestrel()
                .UseStartup(typeof(LivenessProbeStartup))
                .Build();
        }

    }
}
