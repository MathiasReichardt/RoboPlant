using System;
using System.Threading.Tasks;
using RoboPlant.Util.PatternMatching;

namespace RoboPlant.Application.Results
{
    public abstract class OperationAvailableResult
    {
        public void Match(
            Action<Available> available,
            Action<NotAvailable> notAvailable,
            Action<NotReachable> notReachable,
            Action<Error> error)
            => this.TypeMatch(available, notAvailable, notReachable, error);

        public TMatchResult Match<TMatchResult>(
            Func<Available, TMatchResult> available,
            Func<NotAvailable, TMatchResult> notAvailable,
            Func<NotReachable, TMatchResult> notReachable,
            Func<Error, TMatchResult> error)
            => this.TypeMatch(available, notAvailable, notReachable, error);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Available, Task<TMatchResult>> available,
            Func<NotAvailable, Task<TMatchResult>> notAvailable,
            Func<NotReachable, Task<TMatchResult>> notReachable,
            Func<Error, Task<TMatchResult>> error)
            => this.TypeMatch(available, notAvailable, notReachable, error);


        public sealed class Available : OperationAvailableResult
        {
        }

        public sealed class NotAvailable : OperationAvailableResult
        {
        }

        public sealed class NotReachable : OperationAvailableResult
        {
        }

        public sealed class Error : OperationAvailableResult
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}
