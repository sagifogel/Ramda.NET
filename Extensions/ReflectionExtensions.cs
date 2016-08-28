using System;
using System.Reflection;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        internal static TArg CastTo<TArg>(this object arg) {
            if (arg != null && typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        internal static bool IsNull(this object target) {
            return ReferenceEquals(target, null);
        }

        internal static bool IsNotNull(this object target) {
            return !target.IsNull();
        }

        internal static object Member(this object target, string name) {
            var members = target.GetType().GetMember(name, bindingFlags);

            if (members.Length == 1) {
                var member = members[0];

                switch (member.MemberType) {
                    case MemberTypes.Field:
                        return ((FieldInfo)member).GetValue(target);
                    case MemberTypes.Property:
                        return ((PropertyInfo)member).GetValue(target);
                     default:
                        break;
                }
            }

            return null;
        }
    }
}
