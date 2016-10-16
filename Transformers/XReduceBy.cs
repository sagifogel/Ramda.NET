using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal class XReduceBy : XFBase<ITransformer>, ITransformer
    {
        private readonly IList valueAcc;
        private readonly Func<object, string> keyFn;
        private IDictionary<string, object[]> inputs;
        private readonly Func<object, object, object> valueFn;

        internal XReduceBy(Func<object, object, object> valueFn, IList valueAcc, Func<object, string> keyFn, ITransformer xf) : base(xf) {
            this.keyFn = keyFn;
            this.valueFn = valueFn;
            this.valueAcc = valueAcc;
            inputs = new Dictionary<string, object[]>();
        }

        public override object Result(object result) {
            foreach (var key in inputs.Keys) {
                var stepResult = xf.Step(result, inputs[key]) as IReduced;

                if (stepResult is IReduced) {
                    result = stepResult.Value;
                    break;
                }
            }

            inputs = null;

            return xf.Result(result);
        }

        public object Step(object result, object input) {
            var key = keyFn(input);

            inputs[key] = inputs[key] ?? new object[2] { key, valueAcc };
            inputs[key][1] = valueFn(inputs[key][1], input);

            return result;
        }
    }
}
