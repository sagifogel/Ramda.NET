using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class PreservingReduced : XFBase<ITransformer>, ITransformer
    {
        internal PreservingReduced(ITransformer xf) : base(xf) {
        }

        public object Step(object result, object input) {
            var ret = xf.Step(result, input);
            var transformer = ret as IReduced;

            return transformer.IsNotNull() && transformer.IsReduced ? ForceReduced(ret) : ret;
        }
    }
}
