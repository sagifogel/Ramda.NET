using System;
using System.Dynamic;

namespace Ramda.NET
{
    public abstract class AbstractLambda : DynamicDelegate
    {
        protected readonly dynamic fn;

        public AbstractLambda(DynamicDelegate fn, int? length = null) {
            this.fn = fn;
            Length = length ?? fn.Length;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
            result = TryInvoke(binder, args);
            return true;
        }

        protected abstract object TryInvoke(InvokeBinder binder, object[] args);

        public override Delegate Unwrap() {
            return fn.Unwrap();
        }
    }
}
