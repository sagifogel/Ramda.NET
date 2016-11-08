using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Ramda.NET
{
    class CurriedDelegate : DynamicDelegate
    {
        private readonly dynamic @delegate;

        internal CurriedDelegate(int length, DynamicDelegate fn) {
            @delegate = fn;
            Length = length;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = @delegate(arguments.Slice(0, Length));
            return true;
        }
    }
}
