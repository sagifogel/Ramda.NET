using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using static Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    public class Curry1 : AbstractLambda
    {
        public Curry1(DynamicDelegate fn) : base(fn, 1) {
        }

        public Curry1(Delegate fn) : base(new DelegateDecorator(fn)) {
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            object arg1 = null;
            var length = Currying.Arity(arguments);
            
            if (length == 0 || R.__.Equals(arg1 = arguments[0])) {
                return Curry1(fn);
            }

            return fn(arg1);
        }
    }
}
