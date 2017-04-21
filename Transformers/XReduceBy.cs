using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using static Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal class XReduceBy : XFBase<ITransformer>, ITransformer
    {
        private readonly dynamic keyFn;
        private readonly object valueAcc;
        private readonly dynamic valueFn;
        private IDictionary<string, object[]> inputs;

        internal XReduceBy(DynamicDelegate valueFn, object valueAcc, dynamic keyFn, ITransformer xf) : base(xf) {
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

            inputs[key] = inputs.ContainsKey(key) ? inputs[key] : new object[2] { key, valueAcc };
            inputs[key][1] = DynamicInvoke(valueFn, new[] { inputs[key][1], input });

            return result;
        }
    }
}
