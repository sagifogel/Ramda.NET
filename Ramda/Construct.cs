using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public static dynamic Construct<TTarget>(Func<TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1>(Func<TArg1, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2>(Func<TArg1, TArg2, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        public static dynamic Construct(dynamic Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }
    }
}