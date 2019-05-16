﻿using System.Buffers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;
using Bluehands.Hypermedia.MediaTypes;
using Microsoft.AspNetCore.Mvc.Formatters;
using RoboPlant.Application.Persistence;
using RoboPlant.Application.Production;
using RoboPlant.InMemoryPersistence;
using RoboPlant.Server.GlopbalExceptionHandler;
using RoboPlant.Server.Problems;

namespace RoboPlant.Server
{
    public class Startup
    {
       // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // we will use only lower case in URL paths
            services.AddRouting(options => options.LowercaseUrls = true);
            var builder = services.AddMvcCore(options =>
            {
                options.OutputFormatters.Clear();
                options.OutputFormatters.Add(new JsonOutputFormatter(
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    }, ArrayPool<char>.Shared));

                // Initializes and adds the Hypermedia Extensions
                options.AddHypermediaExtensions(
                    hypermediaOptions: new HypermediaExtensionsOptions
                    {
                        ReturnDefaultRouteForUnknownHto = true
                    });
            });

            // Infrastructure
            services.AddCors();
            services.AddSingleton<IProblemFactory, ProblemFactory>();
            
            builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter(services)); });

            // Required by Hypermedia Extensions
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // DI for application
            services.AddSingleton<IProductionLineRepository, ProductionLineRepository>();
            services.AddTransient<ProductionCommandHandler>();
            services.AddTransient<ProductionLineCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IProblemFactory problemFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Location");
                }
            );
            app.UseMvc();

            // unknown route
            app.Run(async context =>
            {
                var problem = problemFactory.NotFound();
                context.Response.ContentType = DefaultMediaTypes.ProblemJson;
                context.Response.StatusCode = problem.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problem));
            });
        }
    }
}