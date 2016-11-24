using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal class DelegateDecorator : DynamicDelegate
    {
        private readonly Delegate @delegate;

        internal DelegateDecorator(Delegate fn, int? length = null) {
            @delegate = fn;
            Length = length ?? fn.Method.GetParameters().Length;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = @delegate.Invoke(arguments);
            return true;
        }
    }
}
