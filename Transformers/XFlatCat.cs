using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class XFlatCat : XFBase<ITransformer>, ITransformer
    {
        internal XFlatCat(ITransformer xf) : base(new PreservingReduced(xf)) {
        }

        public object Step(object result, object input) {
            return !IsArrayLike(input) ? ReduceInternal(xf, result, new[] { input }) : ReduceInternal(xf, result, input);
        }
    }
}