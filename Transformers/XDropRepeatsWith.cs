using System;

namespace Ramda.NET
{
    internal class XDropRepeatsWith : XFBase<ITransformer>, ITransformer
    {
        private object lastValue;
        private bool seenFirstValue = false;
        private Func<object, object, bool> pred;

        internal XDropRepeatsWith(Func<object, object, bool> pred, ITransformer xf) : base(xf) {
            this.pred = pred;
        }

        public object Step(object result, object input) {
            var sameAsLast = false;

            if (!seenFirstValue) {
                seenFirstValue = true;
            }
            else if (pred(lastValue, input)) {
                sameAsLast = true;
            }

            lastValue = input;

            return sameAsLast ? result : xf.Step(result, input);
        }
    }
}
