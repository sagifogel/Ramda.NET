using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    public class Curry3 : AbstractLambda
    {
        public Curry3(object fn) : base(fn, 3) {
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            var length = arguments.Length;

            if (length == 0) {
                return new Curry3(fn);
            }
            else {
                object arg2;
                object arg3;
                object arg1 = arguments[0];
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arg3IsPlaceHolder = false;

                switch (length) {
                    case 1:
                        return IsPlaceholder(arg1) ? (AbstractLambda)new Curry2(fn) : new Curry1(new Func<object, object>(_arg2 => fn.Invoke(arg1, _arg2)));
                    case 2:
                        arg2 = arguments[1];
                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);

                        return arg1IsPlaceHolder && arg2IsPlaceHolder ? (AbstractLambda)new Curry2(fn) : arg1IsPlaceHolder ? new Curry1(new Func<object, object>(_arg1 => {
                            return fn.Invoke(_arg1, arg2);
                        })) : arg2IsPlaceHolder ? new Curry1(new Func<object, object>(_arg2 => {
                            return fn.Invoke(arg1, _arg2);
                        })) : fn.Invoke(arg1, arg2);
                default:
                        arg2 = arguments[1];
                        arg3 = arguments[2];
                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);
                        arg3IsPlaceHolder = IsPlaceholder(arg3);

                        return arg1IsPlaceHolder && arg2IsPlaceHolder && arg3IsPlaceHolder ? new Curry3(fn) : arg1IsPlaceHolder && arg2IsPlaceHolder ? new Curry2(new Func<object, object, object>((_arg1, _arg2) => {
                            return fn.Invoke(_arg1, _arg2, arg3);
                        })) : arg1IsPlaceHolder && arg3IsPlaceHolder ? new Curry2(new Func<object, object, object>((_arg1, _arg3) => {
                            return fn.Invoke(_arg1, arg2, _arg3);
                        })) : arg2IsPlaceHolder && arg3IsPlaceHolder ? new Curry2(new Func<object, object, object>((_arg2, _arg3) => {
                            return fn.Invoke(arg1, _arg2, _arg3);
                        })) : arg1IsPlaceHolder ? new Curry1(new Func<object, object>(_arg1 => {
                            return fn.Invoke(_arg1, arg2, arg3);
                        })) : arg2IsPlaceHolder ? new Curry1(new Func<object, object>(_arg2 => {
                            return fn.Invoke(arg1, _arg2, arg3);
                        })) : arg3IsPlaceHolder ? new Curry1(new Func<object, object>(_arg3 => {
                            return fn.Invoke(arg1, arg2, _arg3);
                        })) : fn.Invoke(arg1, arg2, arg3);
                }
            }
        }
    }
}
