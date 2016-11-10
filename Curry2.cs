using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    public class Curry2 : AbstractLambda
    {
        public Curry2(DynamicDelegate fn) : base(fn, 2) {
        }

        public Curry2(Delegate fn) : base(new DelegateDecorator(fn)) {
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            object arg1;
            var length = Arity(arguments);

            switch (length) {
                case 0:
                    return Curry2(fn);
                case 1:
                    return IsPlaceholder(arg1 = arguments[0]) ? Curry2(fn) : Curry1(_arg2 => fn(arg1, _arg2));
                default:
                    var arg2 = arguments[1];
                    var arg2IsPlaceHolder = IsPlaceholder(arg2);
                    var arg1IsPlaceHolder = IsPlaceholder(arg1 = arguments[0]);

                    return arg1IsPlaceHolder && arg2IsPlaceHolder ? Curry2(fn) : arg1IsPlaceHolder ? Curry1(_arg1 => {
                        return fn(_arg1, arg2);
                    }) : arg2IsPlaceHolder ? Curry1(_arg2 => {
                        return fn(arg1, _arg2);
                    }) : fn(arg1, arg2);
            }
        }
    }
}
