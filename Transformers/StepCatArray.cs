using System;
using System.Collections;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class StepCatArray : ITransformer
    {
        public object Init() {
            return new ArrayList();
        }

        public object Result(object result) {
            var arrayList = result as ArrayList;

            if (arrayList.IsNotNull()) {
                result = arrayList.ToArray();
            }

            return IdentityInternal(result);
        }

        public object Step(object result, object input) {
            var arrayList = result.IsArray() ? new ArrayList((Array)result) : (ArrayList)result;

            arrayList.Add(input);

            return arrayList;
        }
    }
}
