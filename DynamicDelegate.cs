using System;
using System.Dynamic;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    public abstract class DynamicDelegate : DynamicObject
    {
        public int Length { get; protected set; }

        public TResult DynamicInvoke<TResult>(params object[] arguments) {
            return (TResult)Reflection.DynamicInvoke((dynamic)this, arguments);
        }

        public object DynamicInvoke(params object[] arguments) {
            return Reflection.DynamicInvoke((dynamic)this, arguments);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            if (binder.Name.Equals("Length")) {
                result = Length;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public int Arity() {
            return Length;
        }

        public abstract Delegate Unwrap();
    }
}
