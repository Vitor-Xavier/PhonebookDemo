﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Phonebook.Common;
using Phonebook.Context;
using Phonebook.Extensions;
using Phonebook.Middlewares;
using Prometheus;
using System.Collections.Generic;
using System.IO.Compression;

namespace Phonebook
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<PhonebookContext>(options =>
                options.UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(Configuration.GetConnectionString("PhonebookDatabase")));

            services.AddControllers();
            services.AddHealthChecks().AddDbContextCheck<PhonebookContext>();
            services.ConfigureScopes();
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureAuthentication(Configuration);
            services.AddMemoryCache();

            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("Development environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PhonebookContext>();
                context.Database.EnsureCreated();
            }

            app.UseResponseCompression();
            app.UseCors(CommonKeys.CorsPolicy);
            app.UseHealthChecks("/health");

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    if (!env.IsDevelopment())
                        swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "/phonebook" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(env.IsDevelopment() ? "/swagger/v1/swagger.json" : "/phonebook/swagger/v1/swagger.json", "Phonebook V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();
            app.UseMetricServer();
            app.UseRequestMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
