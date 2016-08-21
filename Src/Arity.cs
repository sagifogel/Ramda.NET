using System;
using System.Linq;

namespace Ramda.NET
{
    public static partial class R
    {
        private static int Arity(params object[] arguments) {
            return arguments.Sum(arg => Convert.ToInt32(arg != null));
        }

        private static Func<TResult> Arity<TResult>(int length, Func<TResult> fn) {
            return () => fn();
        }

        private static Func<TArg1, TResult> Arity<TArg1, TResult>(int length, Func<TArg1, TResult> fn) {
            return (TArg1 arg1) => fn(arg1);
        }

        private static Func<TArg1, TArg2, TResult> Arity<TArg1, TArg2, TResult>(int length, Func<TArg1, TArg2, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2) => fn(arg1, arg2);
        }

        private static Func<TArg1, TArg2, TArg3, TResult> Arity<TArg1, TArg2, TArg3, TResult>(int length, Func<TArg1, TArg2, TArg3, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3) => fn(arg1, arg2, arg3);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) => fn(arg1, arg2, arg3, arg4);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) => fn(arg1, arg2, arg3, arg4, arg5);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) => fn(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) => fn(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) => fn(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) => fn(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        private static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> Arity<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TResult> fn) {
            return (TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9, TArg10 arg10) => fn(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }
    }
}
