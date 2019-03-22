using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CharacterRestService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //// build config
            //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            //    ?? "Production";
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true)
            //    .AddJsonFile($"appsettings.{environment}.json", optional: true)
            //    .AddEnvironmentVariables()
            //    .Build();

            //// configure logger
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            //try
            //{
            //    Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Web host terminated unexpectedly");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                /*.UseSerilog()*/;
    }
}
