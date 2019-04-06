using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.Problems
{
    public class ProblemFactory : IProblemFactory
    {
        private const string RoboPlantProblemTypeNamespace = "RoboPlant";

        private bool AddExceptionInformation { get; }

        public ProblemFactory(IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                this.AddExceptionInformation = true;
            }
        }

        public ProblemJson NotFound()
        {
            return  new ProblemJson
            {
                Title = "Route not found",
                Detail = "Requested a route which is not valid.",
                ProblemType = RoboPlantProblemTypeNamespace + ".RouteNotFound",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        public ExceptionProblemJson Exception(HypermediaFormatterException hypermediaFormatterException)
        {
            return new ExceptionProblemJson(hypermediaFormatterException)
            {
                Title = "Hypermedia formatter error.",
                ProblemType = RoboPlantProblemTypeNamespace+ ".HyperrmediaFormatterError",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Detail = this.AddExceptionDetail(hypermediaFormatterException)
            };
        }

        public ExceptionProblemJson Exception(HypermediaException hypermediaException)
        {
            return new ExceptionProblemJson(hypermediaException)
            {
                Title = "Hypermedia error.",
                ProblemType = RoboPlantProblemTypeNamespace + ".HyperrmediaError",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Detail = this.AddExceptionDetail(hypermediaException)
            };
        }

        public ExceptionProblemJson Generic(Exception exception)
        {
            return new ExceptionProblemJson(exception)
            {
                Title = "Sorry, something went wrong.",
                ProblemType = RoboPlantProblemTypeNamespace + ".InternalError",
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Detail = this.AddExceptionDetail(exception)
            };
        }

        private string AddExceptionDetail(Exception exception)
        {
            if (AddExceptionInformation)
            {
                return exception.ToString();
            }

            return string.Empty;
        }
    }
}
