using System;

namespace Ramda.NET
{
    public static partial class R
	{
        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(RamdaPlaceholder xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, RamdaPlaceholder fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, Func<TAccumulator, ITransformer> fn, RamdaPlaceholder acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(RamdaPlaceholder xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(dynamic xf, RamdaPlaceholder fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce(dynamic xf, dynamic fn, RamdaPlaceholder acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(dynamic xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        /// <summary>
		/// Initializes a transducer using supplied iterator function. Returns a singleitem by iterating through the list, successively calling the transformediterator function and passing it an accumulator value and the current valuefrom the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It will bewrapped as a transformer to initialize the transducer. A transformer can bepassed directly in place of an iterator function. In both cases, iterationmay be stopped early with the `R.reduced` function.A transducer is a function that accepts a transformer and returns atransformer and can be composed directly.A transformer is an an object that provides a 2-arity reducing iteratorfunction, step, 0-arity initial value function, init, and 1-arity resultextraction function, result. The step function is used as the iteratorfunction in reduce. The result function is used to convert the finalaccumulator into the return type and in most cases is R.identity. The initfunction can be used to provide an initial accumulator, but is ignored bytransduce.The iteration is performed with R.reduce after initializing the transducer.
		/// <para />
		/// sig: (c -> c) -> (a,b -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="xf">The transducer function. Receives a transformer and returns a transformer.</param>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array. Wrapped as transformer, if necessary, and used to       initialize the transducer</param>
		/// <param name="acc">The initial accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduce"/>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.Into"/>
        public static dynamic Transduce<TAccumulator>(dynamic xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }
    }
}