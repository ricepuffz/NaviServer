using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NaviServer.Code.Game;

namespace NaviServer
{
    public class Program
    {
        public static string JWTSecretKey { get; private set; }

        public static void Main(string[] args)
        {
            try
            {
                JWTSecretKey = System.IO.File.ReadAllText(Environment.GetEnvironmentVariable("JWT_SECRET_LOC"));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return;
            }

            Ticker.Run();
            CreateHostBuilder(args).Build().Run();
            Ticker.Stop();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
