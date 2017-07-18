using System;

namespace Ramda.NET
{
    public static partial class R
    {
        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget>(Func<TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1>(Func<TArg1, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2>(Func<TArg1, TArg2, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        /// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        public static dynamic Construct<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TTarget> Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }

        /// <summary>
        /// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
        /// <para />
        /// sig: (* -> {*}) -> (* -> {*})
        /// </summary>
        /// <param name="fn">The constructor function to wrap.</param>
        public static dynamic Construct(dynamic Fn) {
            return Currying.Construct(new DelegateDecorator(Fn));
        }
    }
}