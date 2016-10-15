using System;
using System.Collections;

namespace Ramda.NET
{
    internal class StepCatArray : ITransformer
    {
        public object Init() {
            return new ArrayList();
        }

        public object Result(object result) {
            return Core.Identity((IList)result).ToArray<Array>();
        }

        public object Step(object result, object input) {
            ((ArrayList)result).Add(input);

            return result;
        }
    }
}
