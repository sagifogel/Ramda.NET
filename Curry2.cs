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
            var arg1IsPlaceHolder = false;
            var arg2IsPlaceHolder = false;
            var length = arguments.Length;

            if (length == 0) {
                return new Curry2(fn);

            }
            else {
                var arg1 = arguments[0];

                switch (arguments.Length) {
                    case 1:
                        return IsPlaceholder(arg1) ? (AbstractLambda)new Curry2(fn) : new Curry1(new Func<object, object>(_arg2 => fn(arg1, _arg2)));
                    default:
                        var arg2 = arguments[1];

                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);

                        return arg1IsPlaceHolder && arg2IsPlaceHolder ? (AbstractLambda)new Curry2(fn) : arg1IsPlaceHolder ? new Curry1(new Func<object, object>(_arg1 => {
                            return fn(_arg1, arg2);
                        })) : arg2IsPlaceHolder ? new Curry1(new Func<object, object>(_arg2 => {
                            return fn(arg1, _arg2);
                        })) : fn(arg1, arg2);
                }
            }
        }
    }
}
