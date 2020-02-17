using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace MicroMusic.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder<Startup>(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();

                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger();
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath);

                    builder.AddJsonFile("serilog.json", true, true);
                    builder.AddEnvironmentVariables();

                    loggerConfiguration.ReadFrom.Configuration(builder.Build());
                })
                .Build();
            try
            {
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Execution of MicroMusic failed, exceptionmessage: {ex.Message}");
            }
        }
    }
}
