using System;
using System.Dynamic;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public static dynamic Into<TSource>(ExpandoObject acc, Func<ITransformer, ITransformer> xf, IReducible list) {
            return Currying.Into(acc, new DelegateDecorator(xf), list);
        }

        public static dynamic Into(RamdaPlaceholder acc, Func<ITransformer, ITransformer> xf, IReducible list) {
            return Currying.Into(acc, new DelegateDecorator(xf), list);
        }

        public static dynamic Into(ExpandoObject acc, RamdaPlaceholder xf, IReducible list) {
            return Currying.Into(acc, xf, list);
        }

        public static dynamic Into(RamdaPlaceholder acc, dynamic xf, IReducible list) {
            return Currying.Into(acc, xf, list);
        }

        public static dynamic Into(ExpandoObject acc, dynamic xf, IReducible list) {
            return Currying.Into(acc, xf, list);
        }

        public static dynamic Into<TSource, TAccumulator>(IList<TAccumulator> acc, Func<ITransformer, ITransformer> xf, IReducible list) {
            return Currying.Into(acc, new DelegateDecorator(xf), list);
        }

        public static dynamic Into<TAccumulator>(IList<TAccumulator> acc, RamdaPlaceholder xf, IReducible list) {
            return Currying.Into(acc, xf, list);
        }

        public static dynamic Into<TAccumulator>(IList<TAccumulator> acc, dynamic xf, IReducible list) {
            return Currying.Into(acc, xf, list);
        }
    }
}