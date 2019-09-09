﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Phonebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    options.Limits.MaxConcurrentConnections = 10;
                });
    }
}
