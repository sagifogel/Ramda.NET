using System;
using System.Collections;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class XDropLstWhile : AbstractXPredicate
    {
        private ArrayList retained = new ArrayList();
        
        internal XDropLstWhile(DynamicDelegate f, ITransformer xf) : base(f, xf) {
            this.f = f;
        }

        public override object Result(object result) {
            this.retained = null;

            return base.Result(result);
        }

        public override object Step(object result, object input) {
            return f.Invoke(input) ? Retain(result, input) : Flush(result, input);
        }

        private object Retain(object result, object input) {
            retained.Add(input);

            return result;
        }

        private object Flush(object result, object input) {
            result = ReducedInternal(xf.Step(result, retained));
            retained = new ArrayList();

            return xf.Step(result, input);
        }
    }
}
