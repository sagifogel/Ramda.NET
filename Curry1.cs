using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    public class Curry1 : AbstractLambda
    {
        public Curry1(object fn) : base(fn, 1) {
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            if (arguments.Length == 0 || R.__.Equals(arguments[0])) {
                return new Curry1(fn);
            }

            return fn.Invoke(arguments[0]);
        }
    }
}
