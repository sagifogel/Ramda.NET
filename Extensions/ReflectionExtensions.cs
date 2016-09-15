using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;

        internal static TArg CastTo<TArg>(this object arg) {
            if (arg != null && typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        internal static bool IsDelegate(this Type type) {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        internal static bool IsDictionary(this Type type) {
            return typeof(IDictionary).IsAssignableFrom(type);
        }

        internal static bool IsArray(this object value) {
            return value.GetType().IsArray;
        }

        internal static bool IsList(this object value) {
            return typeof(IList).IsAssignableFrom(value.GetType());
        }

        internal static bool IsFunction(this object value) {
            return value.GetType().IsDelegate();
        }

        internal static bool IsNull(this object target) {
            return ReferenceEquals(target, null);
        }

        internal static bool IsNotNull(this object target) {
            return !target.IsNull();
        }

        internal static object Member(this object target, string name) {
            var member = target.TryGetMemberInfo(name);

            if (member.IsNotNull()) {
                switch (member.MemberType) {
                    case MemberTypes.Field:
                        return ((FieldInfo)member).GetValue(target);
                    case MemberTypes.Property:
                        return ((PropertyInfo)member).GetValue(target, null);
                    default:
                        break;
                }
            }

            return null;
        }

        internal static bool TryGetMember(string name, object target, out object memberVal) {
            var member = target.TryGetMemberInfo(name);

            memberVal = null;

            if (member.IsNotNull()) {
                switch (member.MemberType) {
                    case MemberTypes.Field:
                        memberVal = ((FieldInfo)member).GetValue(target);
                        return true;
                    case MemberTypes.Property:
                        memberVal = ((PropertyInfo)member).GetValue(target, null);
                        return true;
                }
            }

            return false;
        }

        internal static object HasMember(this object target, string name) {
            var member = target.TryGetMemberInfo(name);

            if (member.IsNotNull()) {
                switch (member.MemberType) {
                    case MemberTypes.Field:
                        return ((FieldInfo)member).GetValue(target);
                    case MemberTypes.Property:
                        return ((PropertyInfo)member).GetValue(target, null);
                    default:
                        break;
                }
            }

            return null;
        }

        internal static Dictionary<string, object> ToMemberDictionary(this object target) {
            var type = target.GetType();

            return type.GetProperties(bindingFlags)
                       .Cast<MemberInfo>()
                       .Concat(type.GetFields(bindingFlags))
                       .ToDictionary(member => member.Name, m => target.Member(m.Name));
        }

        internal static IEnumerable<MemberInfo> ToMemberInfos(this object target) {
            var type = target.GetType();

            return type.GetProperties(bindingFlags)
                       .Cast<MemberInfo>()
                       .Concat(type.GetFields(bindingFlags));
        }

        internal static MemberInfo TryGetMemberInfo(this object target, string name) {
            var members = target.GetType().GetMember(name, bindingFlags);

            if (members.Length == 1) {
                return members[0];
            }

            return null;
        }

        internal static object GetDefaultValue(this Type type) {
            var @delegate = Expression.Lambda<Func<object>>(
                                Expression.Convert(Expression.Default(type),
                                                    typeof(object))).Compile();

            return ((Func<object>)@delegate)();
        }

        internal static bool IsAnonymousType(this Type type) {
            return type.IsClass &&
                   type.IsSealed &&
                   type.Attributes.HasFlag(TypeAttributes.NotPublic) &&
                   type.IsDefined(typeof(CompilerGeneratedAttribute), true);
        }

        internal static ConstructorInfo GetConstructor(this object value, out IEnumerable<Type> parameters) {
            var type = value.GetType();
            var ctor = type.GetConstructor(Type.EmptyTypes);

            parameters = new List<Type>();

            if (ctor.IsNotNull()) {
                return ctor;
            }

            ctor = type.GetConstructors()[0];
            parameters = ctor.GetParameters().Select(param => param.ParameterType);

            return ctor;
        }

        internal static IList CreateNewArray(this object[] array, int len) {
            Type lastType = array[0].GetType();
            var allSameType = array.All(el => lastType.Equals(el.GetType()));

            if (allSameType) {
                return lastType.CreateNewArray<IList>(len);
            }

            return array.GetType().CreateNewArray<IList>(len);
        }

        internal static IList CreateNewArray(this IList array, int? len = null) {
            return array.Cast<object>().ToArray().CreateNewArray(len ?? array.Count);
        }

        internal static ListType CreateNewArray<ListType>(this Type type, int len) {
            return (ListType)type.MakeArrayType(1).GetConstructors()[0].Invoke(new object[] { len }); ;
        }

        internal static ListType CreateNewList<ListType>(this Type type) {
            return (ListType)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
        }

        internal static IList CreateNewList(this IEnumerable list, IEnumerable<object> elements = null) {
            Type type;
            var listType = list.GetType();

            if (listType.HasElementType) {
                type = listType.GetElementType();
            }
            else if (listType.IsGenericType) {
                type = listType.GetGenericArguments()[0];
            }
            else {
                type = typeof(object);
            }

            if (elements != null) {
                return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(new Type[] { typeof(IEnumerable<>).MakeGenericType(type) }).Invoke(new object[] { elements });
            }

            return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        internal static IList CreateNewListOfType(this IEnumerable list, Type type) {
            return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        internal static Func<object> GetFactory(this object value) {
            IEnumerable<Type> parameters;
            var ctor = value.GetConstructor(out parameters);
            var arguments = parameters.Select(param => Expression.Constant(param.GetDefaultValue(), param));

            return Expression.Lambda<Func<object>>(
                        Expression.Convert(Expression.New(ctor, arguments), typeof(object))).Compile();
        }

        internal static object ToInvokable(this object target) {
            if (target.GetType().IsArray) {
                return ((IList)target).Cast<object>().ToArray();
            }

            return target;
        }

        public static bool Is<TCompareTo>(this object @object) {
            return typeof(TCompareTo).IsAssignableFrom(@object.GetType());
        }
    }
}
