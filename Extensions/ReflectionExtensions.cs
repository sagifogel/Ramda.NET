using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

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
            var member = target.TryGetMember(name);

            if (member.IsNotNull()) {
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

        internal static MemberInfo TryGetMember(this object target, string name) {
            var members = target.GetType().GetMember(name, bindingFlags);

            if (members.Length == 1) {
                return members[0];
            }

            return null;
        }

        internal static bool IsAnonymousType(this Type type) {
            return type.IsClass &&
                   type.IsSealed &&
                   type.Attributes.HasFlag(TypeAttributes.NotPublic) &&
                   type.IsDefined(typeof(CompilerGeneratedAttribute), true);
        }
    }
}
