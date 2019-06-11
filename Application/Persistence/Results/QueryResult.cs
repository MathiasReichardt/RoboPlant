using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Util.PatternMatching;

namespace RoboPlant.Application.Persistence.Results
{
    public abstract class QueryResult<TResult>
    {
        public void Match(
            Action<Success> success,
            Action<NotReachable> notReachable,
            Action<InvalidQuery> invalidQuery,
            Action<Error> error)
            => this.TypeMatch(success, notReachable, invalidQuery, error);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<NotReachable, TMatchResult> notReachable,
            Func<InvalidQuery, TMatchResult> invalidQuery,
            Func<Error, TMatchResult> error)
            => this.TypeMatch(success, notReachable, invalidQuery, error);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<NotReachable, Task<TMatchResult>> notReachable,
            Func<InvalidQuery, Task<TMatchResult>> invalidQuery,
            Func<Error, Task<TMatchResult>> error)
            => this.TypeMatch(success, notReachable, invalidQuery, error);


        public sealed class Success : QueryResult<TResult>
        {
            public long TotalEntities { get; }

            public ICollection<TResult> Result { get; }

            public Success(ICollection<TResult> result, long totalEntities)
            {
                Result = result;
                TotalEntities = totalEntities;
            }
        }

        public sealed class NotReachable : QueryResult<TResult>
        {
            public NotReachable()
            {
            }
        }

        public sealed class InvalidQuery : QueryResult<TResult>
        {
            public string Message { get; }

            public InvalidQuery(string message)
            {
                Message = message;
            }
        }

        public sealed class Error : QueryResult<TResult>
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}
