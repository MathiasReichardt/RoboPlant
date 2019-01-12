using System;
using System.Net;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.REST.Problems
{
    public static class ProblemFactory
    {
        private const string RoboPlantProblemTypeNamespace = "RoboPlant";

        public static ProblemJson NotFound()
        {
            return  new ProblemJson
            {
                Title = "Route not found",
                Detail = "Requested a route which is not valid.",
                ProblemType = RoboPlantProblemTypeNamespace + ".RouteNotFound",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        public static ExceptionProblemJson Exception(HypermediaFormatterException hypermediaFormatterException)
        {
            return new ExceptionProblemJson(hypermediaFormatterException)
            {
                Title = "Hypermedia formatter error.",
                ProblemType = RoboPlantProblemTypeNamespace+ ".HyperrmediaFormatterError",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        internal static ExceptionProblemJson Exception(HypermediaException hypermediaException)
        {
            return new ExceptionProblemJson(hypermediaException)
            {
                Title = "Hypermedia error.",
                ProblemType = RoboPlantProblemTypeNamespace + ".HyperrmediaError",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        public static ExceptionProblemJson Generic(Exception exception)
        {
            return new ExceptionProblemJson(exception)
            {
                Title = "Sorry, something went wrong.",
                ProblemType = RoboPlantProblemTypeNamespace + ".InternalError",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
