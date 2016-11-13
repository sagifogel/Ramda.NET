using System;
using System.Collections;
using System.Dynamic;
using static Ramda.NET.ObjectAssigner;

namespace Ramda.NET
{
    internal class StepCatObject : ITransformer
    {
        public object Init() {
            return new ExpandoObject();
        }

        public object Result(object result) => result;

        public object Step(object result, object input) {
            if (R.IsArrayLike(input)) {
                var arr = (IList)input;

                input = R.ObjOf(arr[0].ToString(), arr[1]);
            }

            return Assign((ExpandoObject)result, input);
        }
    }
}
