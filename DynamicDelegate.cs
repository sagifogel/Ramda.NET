using System.Dynamic;

namespace Ramda.NET
{
    public abstract class DynamicDelegate : DynamicObject
    {
        public int Length { get; protected set; }

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
    }
}
