using System;
using static Ramda.NET.Core;

namespace Ramda.NET
{
    internal abstract class AbstractXAnyOrAll : XFBase<ITransformer>, ITransformer
    {
        private bool allOrAny;
        private readonly Func<object, bool> f;
        protected readonly Func<bool, bool> stepPredicate;
        protected readonly Func<bool, bool> resultPredicate;

        internal AbstractXAnyOrAll(Func<object, bool> f, ITransformer xf, bool allOrAny, Func<bool, bool> stepPredicate, Func<bool, bool> resultPredicate) : base(xf) {
            this.f = f;
            this.allOrAny = allOrAny;
            this.stepPredicate = stepPredicate;
            this.resultPredicate = resultPredicate;
        }

        public override object Result(object result) {
            if (resultPredicate(allOrAny)) {
                result = xf.Step(result, allOrAny);
            }

            return base.Result(result);
        }

        public object Step(object result, object input) {
            if (stepPredicate(f.Invoke(input))) {
                allOrAny = !allOrAny;
                result = Reduced(xf.Step(result, allOrAny));
            }

            return result;
        }
    }
}
