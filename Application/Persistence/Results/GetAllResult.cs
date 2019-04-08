using System;
using System.Threading.Tasks;
using Util.PatternMatching;

namespace RoboPlant.Application.Persistence.Results
{
    public abstract class GetAllResult<TResult>
    {
        public void Match(
            Action<Success> success,
            Action<NotReachable> notReachable,
            Action<Error> error)
            => this.TypeMatch(success, notReachable, error);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<NotReachable, TMatchResult> notReachable,
            Func<Error, TMatchResult> error)
            => this.TypeMatch(success, notReachable, error);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<NotReachable, Task<TMatchResult>> notReachable,
            Func<Error, Task<TMatchResult>> error)
            => this.TypeMatch(success, notReachable, error);


        public sealed class Success : GetAllResult<TResult>
        {
            public TResult Result { get; }

            public Success(TResult result)
            {
                Result = result;
            }
        }

        public sealed class NotReachable : GetAllResult<TResult>
        {
            public NotReachable()
            {
            }
        }

        public sealed class Error : GetAllResult<TResult>
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }

    }
}
