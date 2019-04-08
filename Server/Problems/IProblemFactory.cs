using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.HypermediaExtensions.ErrorHandling;
using WebApi.HypermediaExtensions.Exceptions;

namespace RoboPlant.Server.Problems
{
    public interface IProblemFactory
    {
        ExceptionProblemJson Exception(HypermediaFormatterException hypermediaFormatterException);

        ExceptionProblemJson Exception(HypermediaException hypermediaException);

        ExceptionProblemJson Exception(Exception exception);

        ProblemJson NotFound();

        ProblemJson EntityNotFound(string name, string id);

        ProblemJson ServiceUnavailable();
    }
}