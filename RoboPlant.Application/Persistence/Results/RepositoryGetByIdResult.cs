using System;
using System.Threading.Tasks;
using Util.PatternMatching;

namespace RoboPlant.Application.Persistence.Results
{
    public abstract class RepositoryGetByIdResult<TResult>
    {
        public void Match(
            Action<Success> success,
            Action<NotFound> notFound,
            Action<NotReachable> notReachable,
            Action<Error> error)
            => this.TypeMatch(success, notFound, notReachable, error);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<NotFound, TMatchResult> notFound,
            Func<NotReachable, TMatchResult> notReachable,
            Func<Error, TMatchResult> error)
            => this.TypeMatch(success, notFound, notReachable, error);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<NotFound, Task<TMatchResult>> notFound,
            Func<NotReachable, Task<TMatchResult>> notReachable,
            Func<Error, Task<TMatchResult>> error)
            => this.TypeMatch(success, notFound, notReachable, error);


        public sealed class Success : RepositoryGetByIdResult<TResult>
        {
            public TResult Result { get; }

            public Success(TResult result)
            {
                Result = result;
            }
        }

        public sealed class NotFound : RepositoryGetByIdResult<TResult>
        {
            public NotFound()
            {
            }
        }

        public sealed class NotReachable : RepositoryGetByIdResult<TResult>
        {
            public NotReachable()
            {
            }
        }

        public sealed class Error : RepositoryGetByIdResult<TResult>
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }

    }

    public static class TestResultsProducer
    {
        public static RepositoryGetByIdResult<int> GetResult()
        {
            {
                return new RepositoryGetByIdResult<int>.NotFound();
            }
        }
    }
}
