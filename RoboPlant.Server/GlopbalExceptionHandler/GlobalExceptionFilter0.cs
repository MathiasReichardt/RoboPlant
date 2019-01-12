using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RoboPlant.Server.REST.Problems;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.GlopbalExceptionHandler
{
    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        private readonly ILogger logger;

        public GlobalExceptionFilter(ILoggerFactory logger)
        {
            if (logger != null)
            {
                this.logger = logger.CreateLogger("Global Exception Filter");
            }
        }

        public void OnException(ExceptionContext context)
        {
            logger?.LogError("GlobalExceptionFilter", context.Exception);

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
                    exceptionProblemJson = ProblemFactory.Generic(context.Exception);
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
