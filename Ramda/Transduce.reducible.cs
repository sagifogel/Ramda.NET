using System;

namespace Ramda.NET
{
    public static partial class R
	{
        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(RamdaPlaceholder xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, RamdaPlaceholder fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, Func<TAccumulator, ITransformer> fn, RamdaPlaceholder acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(RamdaPlaceholder xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(dynamic xf, RamdaPlaceholder fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce(dynamic xf, dynamic fn, RamdaPlaceholder acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(dynamic xf, Func<TAccumulator, ITransformer> fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(Func<ITransformer, ITransformer> xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Transduce<TAccumulator>(dynamic xf, dynamic fn, TAccumulator acc, IReducible list) {
            return Currying.Transduce(xf, fn, acc, list);
        }
    }
}