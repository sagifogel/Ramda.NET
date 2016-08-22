using System;
using System.Collections;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static dynamic CurryN = Curry2<int, Delegate, dynamic>((length, fn) => {
            if (length == 1) {
                return Curry1(new Func<object[], dynamic>(fn.DynamicInvoke));
            }

            return Arity(length, InternalCurryN(length, new object[0], fn));
        });

        public readonly static dynamic Add = Curry2<double, double, double>((arg1, arg2) => {
            return arg1 + arg2;
        });

        public readonly static dynamic Adjust = Curry3<Func<dynamic, dynamic>, int, IList, IList>((fn, idx, list) => {
            int start = 0;
            int index = 0;
            IList concatedList = null;

            if (idx >= list.Count || idx < -list.Count) {
                return list;
            }

            start = idx < 0 ? list.Count : 0;
            index = start + idx;
            concatedList = InternalConcat(list);
            concatedList[index] = fn(list[index]);

            return concatedList;
        });
    }
}
