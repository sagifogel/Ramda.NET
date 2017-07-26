using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal class Curry3 : AbstractLambda
    {
        internal Curry3(DynamicDelegate fn) : base(fn, 3) {
        }

        internal Curry3(Delegate fn) : base(new DelegateDecorator(fn)) {
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            object arg1 = null;
            object arg2 = null;
            object arg3 = null;
            var arg1IsPlaceHolder = false;
            var arg2IsPlaceHolder = false;
            var arg3IsPlaceHolder = false;
            var arity = Currying.Arity(arguments);

            if (arity > 0) {
                arg1 = arguments[0];
            }

            switch (arity) {
                case 0:
                    return Curry3(fn);
                case 1:
                    return IsPlaceholder(arg1) ? Curry3(fn) : Curry2((_arg2, _arg3) => fn(arg1, _arg2, _arg3));
                case 2:
                    arg2 = arguments[1];
                    arg1IsPlaceHolder = IsPlaceholder(arg1);
                    arg2IsPlaceHolder = IsPlaceholder(arg2);

                    return (arg1IsPlaceHolder = IsPlaceholder(arg1)) && (arg2IsPlaceHolder = IsPlaceholder(arg2)) ? Curry3(fn) : arg1IsPlaceHolder ? Curry2((_arg1, _arg3) => {
                        return fn(_arg1, arg2, _arg3);
                    }) : arg2IsPlaceHolder ? Curry2((_arg2, _arg3) => {
                        return fn(arg1, _arg2, _arg3);
                    }) : Curry1(_arg3 => {
                        return fn(arg1, arg2, _arg3);
                    });
                default:
                    arg2 = arguments[1];
                    arg3 = arguments[2];
                    arg1IsPlaceHolder = IsPlaceholder(arg1);
                    arg2IsPlaceHolder = IsPlaceholder(arg2);
                    arg3IsPlaceHolder = IsPlaceholder(arg3);

                    return arg1IsPlaceHolder && arg2IsPlaceHolder && arg3IsPlaceHolder ? Curry3(fn) : arg1IsPlaceHolder && arg2IsPlaceHolder ? Curry2((_arg1, _arg2) => {
                        return fn(_arg1, _arg2, arg3);
                    }) : arg1IsPlaceHolder && arg3IsPlaceHolder ? Curry2((_arg1, _arg3) => {
                        return fn(_arg1, arg2, _arg3);
                    }) : arg2IsPlaceHolder && arg3IsPlaceHolder ? Curry2((_arg2, _arg3) => {
                        return fn(arg1, _arg2, _arg3);
                    }) : arg1IsPlaceHolder ? Curry1(_arg1 => {
                        return fn(_arg1, arg2, arg3);
                    }) : arg2IsPlaceHolder ? Curry1(_arg2 => {
                        return fn(arg1, _arg2, arg3);
                    }) : arg3IsPlaceHolder ? Curry1(_arg3 => {
                        return fn(arg1, arg2, _arg3);
                    }) : fn(arg1, arg2, arg3);
            }
        }
    }
}
