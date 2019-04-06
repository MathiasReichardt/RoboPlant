using System;
using System.Threading.Tasks;

namespace Util.PatternMatching
{
    public static class PatternMatchExtension
    {
        public static T TypeMatch<TBase, TDerived1, TDerived2, T>(this TBase target, Func<TDerived1, T> handle1, Func<TDerived2, T> handle2)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return handle1(case1);
                case TDerived2 case2:
                    return handle2(case2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static void TypeMatch<TBase, TDerived1, TDerived2>(this TBase target, Action<TDerived1> handle1, Action<TDerived2> handle2)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    handle1(case1);
                    return;
                case TDerived2 case2:
                    handle2(case2);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static void TypeMatch<TBase, TDerived1, TDerived2, TDerived3>(this TBase target, Action<TDerived1> handle1, Action<TDerived2> handle2, Action<TDerived3> handle3)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    handle1(case1);
                    return;
                case TDerived2 case2:
                    handle2(case2);
                    return;
                case TDerived3 case3:
                    handle3(case3);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static void TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4>(this TBase target, Action<TDerived1> handle1, Action<TDerived2> handle2, Action<TDerived3> handle3, Action<TDerived4> handle4)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    handle1(case1);
                    return;
                case TDerived2 case2:
                    handle2(case2);
                    return;
                case TDerived3 case3:
                    handle3(case3);
                    return;
                case TDerived4 case4:
                    handle4(case4);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static void TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, TDerived5>(this TBase target, Action<TDerived1> handle1, Action<TDerived2> handle2, Action<TDerived3> handle3, Action<TDerived4> handle4, Action<TDerived5> handle5)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
            where TDerived5 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    handle1(case1);
                    return;
                case TDerived2 case2:
                    handle2(case2);
                    return;
                case TDerived3 case3:
                    handle3(case3);
                    return;
                case TDerived4 case4:
                    handle4(case4);
                    return;
                case TDerived5 case5:
                    handle5(case5);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static T TypeMatch<TBase, TDerived1, TDerived2, TDerived3, T>(this TBase target, Func<TDerived1, T> handle1, Func<TDerived2, T> handle2, Func<TDerived3, T> handle3)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return handle1(case1);
                case TDerived2 case2:
                    return handle2(case2);
                case TDerived3 case3:
                    return handle3(case3);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static T TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, T>(this TBase target, Func<TDerived1, T> handle1, Func<TDerived2, T> handle2, Func<TDerived3, T> handle3, Func<TDerived4, T> handle4)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return handle1(case1);
                case TDerived2 case2:
                    return handle2(case2);
                case TDerived3 case3:
                    return handle3(case3);
                case TDerived4 case4:
                    return handle4(case4);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static T TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, TDerived5, T>(this TBase target, Func<TDerived1, T> handle1, Func<TDerived2, T> handle2, Func<TDerived3, T> handle3, Func<TDerived4, T> handle4, Func<TDerived5, T> handle5)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
            where TDerived5 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return handle1(case1);
                case TDerived2 case2:
                    return handle2(case2);
                case TDerived3 case3:
                    return handle3(case3);
                case TDerived4 case4:
                    return handle4(case4);
                case TDerived5 case5:
                    return handle5(case5);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static T TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, TDerived5, TDerived6, T>(this TBase target, Func<TDerived1, T> handle1, Func<TDerived2, T> handle2, Func<TDerived3, T> handle3, Func<TDerived4, T> handle4, Func<TDerived5, T> handle5, Func<TDerived6, T> handle6)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
            where TDerived5 : class, TBase
            where TDerived6 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return handle1(case1);
                case TDerived2 case2:
                    return handle2(case2);
                case TDerived3 case3:
                    return handle3(case3);
                case TDerived4 case4:
                    return handle4(case4);
                case TDerived5 case5:
                    return handle5(case5);
                case TDerived6 case6:
                    return handle6(case6);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static async Task<T> TypeMatch<TBase, TDerived1, TDerived2, T>(this TBase target, Func<TDerived1, Task<T>> handle1, Func<TDerived2, Task<T>> handle2, Func<TBase, Task<T>> unhandled = null)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return await handle1(case1).ConfigureAwait(false);
                case TDerived2 case2:
                    return await handle2(case2).ConfigureAwait(false);
            }

            if (unhandled != null)
            {
                return await unhandled(target).ConfigureAwait(false);
            }
            throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
        }

        public static async Task<T> TypeMatch<TBase, TDerived1, TDerived2, TDerived3, T>(this TBase target, Func<TDerived1, Task<T>> handle1, Func<TDerived2, Task<T>> handle2, Func<TDerived3, Task<T>> handle3, Func<TBase, Task<T>> unhandled = null)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return await handle1(case1).ConfigureAwait(false);
                case TDerived2 case2:
                    return await handle2(case2).ConfigureAwait(false);
                case TDerived3 case3:
                    return await handle3(case3).ConfigureAwait(false);
            }

            if (unhandled != null)
            {
                return await unhandled(target).ConfigureAwait(false);
            }
            throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
        }

        public static async Task TypeMatch<TBase, TDerived1, TDerived2>(this TBase target, Func<TDerived1, Task> handle1, Func<TDerived2, Task> handle2)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    await handle1(case1).ConfigureAwait(false);
                    return;
                case TDerived2 case2:
                    await handle2(case2).ConfigureAwait(false);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static async Task<T> TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, T>(this TBase target, Func<TDerived1, Task<T>> handle1, Func<TDerived2, Task<T>> handle2, Func<TDerived3, Task<T>> handle3, Func<TDerived4, Task<T>> handle4)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return await handle1(case1).ConfigureAwait(false);
                case TDerived2 case2:
                    return await handle2(case2).ConfigureAwait(false);
                case TDerived3 case3:
                    return await handle3(case3).ConfigureAwait(false);
                case TDerived4 case4:
                    return await handle4(case4).ConfigureAwait(false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }

        public static async Task<T> TypeMatch<TBase, TDerived1, TDerived2, TDerived3, TDerived4, TDerived5, T>(this TBase target, Func<TDerived1, Task<T>> handle1, Func<TDerived2, Task<T>> handle2, Func<TDerived3, Task<T>> handle3, Func<TDerived4, Task<T>> handle4, Func<TDerived5, Task<T>> handle5)
            where TDerived1 : class, TBase
            where TDerived2 : class, TBase
            where TDerived3 : class, TBase
            where TDerived4 : class, TBase
            where TDerived5 : class, TBase
        {
            switch (target)
            {
                case TDerived1 case1:
                    return await handle1(case1).ConfigureAwait(false);
                case TDerived2 case2:
                    return await handle2(case2).ConfigureAwait(false);
                case TDerived3 case3:
                    return await handle3(case3).ConfigureAwait(false);
                case TDerived4 case4:
                    return await handle4(case4).ConfigureAwait(false);
                case TDerived5 case5:
                    return await handle5(case5).ConfigureAwait(false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), $"Target has unexpected type {target.GetType().Name}");
            }
        }
    }
}