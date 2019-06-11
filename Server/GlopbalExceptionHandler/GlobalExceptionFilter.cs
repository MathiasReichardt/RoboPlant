using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoboPlant.Server.Problems;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.GlopbalExceptionHandler
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        private ILogger Logger { get; }

        private IProblemFactory ProblemFactory { get; }
        
        public GlobalExceptionFilter(IServiceCollection serviceCollection)
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            if (loggerFactory != null)
            {
                Logger = loggerFactory.CreateLogger("Global Exception Filter");
            }

            ProblemFactory = serviceProvider.GetService<IProblemFactory>();
            if (ProblemFactory == null)
            {
                throw new Exception("Could not get problem factory for global exception handler.");
            }
        }

        public void OnException(ExceptionContext context)
        {
            Logger?.LogError("{Exception}", context.Exception);

            ExceptionProblemJson exceptionProblemJson;
            switch (context.Exception)
            {
                case HypermediaFormatterException hypermediaFormatterException:
                    exceptionProblemJson = ProblemFactory.Exception(hypermediaFormatterException);
                    break;
                case HypermediaException hypermediaException:
                    exceptionProblemJson = ProblemFactory.Exception(hypermediaException);
                    break;
                
                default:
                    exceptionProblemJson = ProblemFactory.Exception(context.Exception);
                    break;
            }
            
            CreateResultObject(context, exceptionProblemJson);
        }

        private static void CreateResultObject(ExceptionContext context, ExceptionProblemJson response)
        {
            context.Result = new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
                DeclaredType = response.GetType()
            };
        }

        public void Dispose()
        {
        }
    }
}
