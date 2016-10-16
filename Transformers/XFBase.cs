namespace Ramda.NET
{
    internal abstract class XFBase<TTansformer> where TTansformer : ITransformerBase
    {
        protected readonly TTansformer xf;

        internal XFBase(TTansformer xf) {
            this.xf = xf;
        }

        public virtual object Init() {
            return xf.Init();
        }

        public virtual object Result(object result) {
            return xf.Result(result);
        }
    }
}
