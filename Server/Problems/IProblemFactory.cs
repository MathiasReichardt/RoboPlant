using System;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.Problems
{
    public interface IProblemFactory
    {
        ExceptionProblemJson Exception(HypermediaFormatterException hypermediaFormatterException);

        ExceptionProblemJson Exception(HypermediaException hypermediaException);

        ExceptionProblemJson Generic(Exception exception);

        ProblemJson NotFound();
    }
}