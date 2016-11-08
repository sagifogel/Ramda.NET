using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    public class CurryN : AbstractLambda
    {
        private int left;
        private readonly object[] received;

        public CurryN(DynamicDelegate fn, object[] received = null, int? left = null) : base(fn) {
            this.received = received ?? new object[0];
            this.left = left ?? length;
        }

        protected override object TryInvoke(InvokeBinder binder, object[] arguments) {
            var argsIdx = 0;
            var combinedIdx = 0;
            var combined = new List<object>();
            var argumentsLength = arguments?.Length ?? 0;

            while (combinedIdx < received.Length || argsIdx < argumentsLength) {
                object result = null;

                if (combinedIdx < received.Length && (!IsPlaceholder(received[combinedIdx]) || argsIdx >= argumentsLength)) {
                    result = received[combinedIdx];
                }
                else {
                    result = arguments[argsIdx];
                    argsIdx += 1;
                }

                combined.Insert(combinedIdx, result);

                if (!IsPlaceholder(result)) {
                    left -= 1;
                }

                combinedIdx += 1;
            }

            return left <= 0 ? fn(combined.ToArray()) : new CurryN(fn, combined.ToArray(), left);
        }
    }
}
