using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public delegate dynamic Lambda2(object arg1 = null, object arg2 = null);

        public static dynamic Curry2<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> fn) {
            return new Lambda2((arg1, arg2) => {
                bool arg1IsPlaceHolder = false;
                bool arg2IsPlaceHolder = false;

                switch (R.Arity(arg1, arg2)) {
                    case 0:
                        return Curry2(fn);
                    case 1:
                        return IsPlaceHolder(arg1) ? Curry2(fn) : Curry1<TArg2, TResult>(_arg2 => fn(arg1.CastTo<TArg1>(), _arg2));
                    default:
                        return (arg1IsPlaceHolder = IsPlaceHolder(arg1)) && (arg2IsPlaceHolder = IsPlaceHolder(arg2)) ? Curry2(fn) : arg1IsPlaceHolder ? Curry1<TArg1, TResult>(_arg1 => {
                            return fn(_arg1, arg2.CastTo<TArg2>());
                        }) : arg2IsPlaceHolder ? Curry1<TArg2, TResult>(_arg2 => {
                            return fn(arg1.CastTo<TArg1>(), _arg2);
                        }) : fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>());
                }
            });
        }
    }
}
