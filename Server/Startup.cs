namespace RoboPlant.Server
{
    using System.Text.Json;

    using Bluehands.Hypermedia.MediaTypes;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using RoboPlant.Application.Design;
    using RoboPlant.Application.Persistence;
    using RoboPlant.Application.Production;
    using RoboPlant.Application.Production.ProductionLine;
    using RoboPlant.InMemoryPersistence;
    using RoboPlant.Server.GlopbalExceptionHandler;
    using RoboPlant.Server.Problems;

    using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // we will use only lower case in URL paths
            services.AddRouting(options => options.LowercaseUrls = true);
            var builder = services.AddMvcCore(
                options =>
                    {
                        options.OutputFormatters.Clear();
                        options.OutputFormatters.Add(
                            new SystemTextJsonOutputFormatter(new JsonSerializerOptions { IgnoreNullValues = true }));
                    });

            // Initializes and adds the Hypermedia Extensions
            builder.Services.AddHypermediaExtensions( o =>
            {
                o.ReturnDefaultRouteForUnknownHto = true;
            });

            // Infrastructure
            services.AddCors();
            services.AddSingleton<IProblemFactory, ProblemFactory>();

            builder.AddMvcOptions(
                o =>
                    {
                        o.Filters.Add(new GlobalExceptionFilter(services));
                    });

            services.AddControllers();

            // DI for application
            services.AddSingleton<IProductionLineRepository, ProductionLineRepository>();

            services.AddTransient<ProductionCommandHandler>();
            services.AddTransient<GetByIdCommandHandler>();
            services.AddTransient<ShutDownForMaintenanceCommandHandler>();
            services.AddTransient<CompleteMaintenanceCommandHandler>();

            services.AddSingleton<IRobotBlueprintRepository, RobotBlueprintRepository>();
            services.AddTransient<DesignCommandHandler>();
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
                });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

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
