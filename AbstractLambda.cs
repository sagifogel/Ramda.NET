using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    public abstract class AbstractLambda : DynamicDelegate
    {
        protected readonly int length;
        protected readonly dynamic fn;

        public AbstractLambda(object fn, int? length = null) {
            if (fn.IsFunction()) {
                this.fn = new DelegateDecorator((Delegate)fn);
            }
            else {
                this.fn = (dynamic)fn;
            }

            Length = length ?? this.fn.Length;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
            result = TryInvoke(binder, args, out result);
            return true;
        }

        protected abstract object TryInvoke(InvokeBinder binder, object[] args);
    }
}
