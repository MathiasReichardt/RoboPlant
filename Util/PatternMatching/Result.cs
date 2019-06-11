using System;
using System.Threading.Tasks;

namespace RoboPlant.Util.PatternMatching
{
    public abstract class Result<TSuccess, TError>
    {
        public void Match(
            Action<Success> success,
            Action<Failure> failure)
            => this.TypeMatch(success, failure);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<Failure, TMatchResult> failure)
            => this.TypeMatch(success, failure);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<Failure, Task<TMatchResult>> failure)
            => this.TypeMatch(success, failure);


        public sealed class Success : Result<TSuccess, TError>
        {
            public TSuccess Result { get; }

            public Success(TSuccess result)
            {
                Result = result;
            }
        }

        public sealed class Failure : Result<TSuccess, TError>
        {
            public TError Error { get; }

            public Failure(TError error)
            {
                Error = error;
            }
        }
    }

    public abstract class Result<TError>
    {
        public void Match(
            Action<Success> success,
            Action<Failure> failure)
            => this.TypeMatch(success, failure);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<Failure, TMatchResult> failure)
            => this.TypeMatch(success, failure);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<Failure, Task<TMatchResult>> failure)
            => this.TypeMatch(success, failure);


        public sealed class Success : Result<TError>
        {
            public Success()
            {
            }
        }

        public sealed class Failure : Result<TError>
        {
            public TError Error { get; }

            public Failure(TError error)
            {
                Error = error;
            }
        }
    }
}
