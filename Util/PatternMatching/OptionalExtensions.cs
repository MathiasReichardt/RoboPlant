using System;
using Optional;

namespace RoboPlant.Util.PatternMatching
{
    public static class OptionalExtensions
    {

        public static bool IsPossible<T>(this Option<Func<T>> option)
        {
            return option.HasValue;
        }

        public static TResult Execute<TParam, TResult>(this Option<TParam, TResult> option, Func<TParam, TResult> some, Func<TParam, TResult> none)
        {
            return default(TResult); /* option.Match(some, none);*/
        }

    }
}
