using System.Buffers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RoboPlant.Server.REST.Problems;
using WebApi.HypermediaExtensions.WebApi.ExtensionMethods;
using Bluehands.Hypermedia.MediaTypes;
using Microsoft.AspNetCore.Mvc.Formatters;
using RoboPlant.Server.GlopbalExceptionHandler;

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
                options.AddHypermediaExtensions();
            });
            builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter(null)); });

            // Required by Hypermedia Extensions
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // unknown route
            app.Run(async context =>
            {
                var problem = ProblemFactory.NotFound();
                context.Response.ContentType = DefaultMediaTypes.ProblemJson;
                context.Response.StatusCode = problem.StatusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problem));
            });
        }
    }
}
