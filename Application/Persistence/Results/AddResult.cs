using System;
using System.Threading.Tasks;
using RoboPlant.Domain.Ids;
using Util.PatternMatching;

namespace RoboPlant.Application.Persistence.Results
{
    public abstract class AddResult<TId> where TId : IdBase
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


        public sealed class Success : AddResult<TId>
        {
            public TId ResultId { get; }

            public Success(TId resultId)
            {
                ResultId = resultId;
            }
        }

        public sealed class NotReachable : AddResult<TId>
        {
            public NotReachable()
            {
            }
        }

        public sealed class Error : AddResult<TId>
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }

    }
}
