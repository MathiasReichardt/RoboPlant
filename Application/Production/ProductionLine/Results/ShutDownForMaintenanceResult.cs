﻿using System;
using System.Threading.Tasks;
using Util.PatternMatching;

namespace RoboPlant.Application.Production.Results
{
    public abstract class ShutDownForMaintenanceResult
    {
        public void Match(
            Action<Success> success,
            Action<NotAvailable> notAvailable,
            Action<NotFound> notFound,
            Action<NotReachable> notReachable,
            Action<Error> error)
            => this.TypeMatch(success, notAvailable, notFound, notReachable, error);

        public TMatchResult Match<TMatchResult>(
            Func<Success, TMatchResult> success,
            Func<NotAvailable, TMatchResult> notAvailable,
            Func<NotFound, TMatchResult> notFound,
            Func<NotReachable, TMatchResult> notReachable,
            Func<Error, TMatchResult> error)
            => this.TypeMatch(success, notAvailable, notFound, notReachable, error);

        public Task<TMatchResult> Match<TMatchResult>(
            Func<Success, Task<TMatchResult>> success,
            Func<NotAvailable, Task<TMatchResult>> notAvailable,
            Func<NotFound, Task<TMatchResult>> notFound,
            Func<NotReachable, Task<TMatchResult>> notReachable,
            Func<Error, Task<TMatchResult>> error)
            => this.TypeMatch(success, notAvailable, notFound, notReachable, error);


        public sealed class Success : ShutDownForMaintenanceResult
        {
            public Success()
            {
            }
        }

        public sealed class NotAvailable : ShutDownForMaintenanceResult
        {
            public NotAvailable()
            {
            }
        }

        public sealed class NotFound : ShutDownForMaintenanceResult
        {
            public NotFound()
            {
            }
        }

        public sealed class NotReachable : ShutDownForMaintenanceResult
        {
            public NotReachable()
            {
            }
        }

        public sealed class Error : ShutDownForMaintenanceResult
        {
            public Exception Exception { get; }

            public Error(Exception exception)
            {
                Exception = exception;
            }
        }
    }
}