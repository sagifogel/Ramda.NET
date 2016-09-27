using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        private static Type[] EmptyTypes = System.Type.EmptyTypes;
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();
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

        internal static bool IsDictionaryOf<TKey, TValue>(this Type type) {
            return typeof(IDictionary<TKey, TValue>).IsAssignableFrom(type);
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

        internal static object Member(this Array target, int index) {
            return target.GetValue(index);
        }

        internal static bool TryGetMember(this Array target, int index, out object memberVal) {
            if (index < target.Length) {
                memberVal = target.Member(index);
                return true;
            }

            memberVal = null;
            return false;
        }

        internal static bool TryGetMember(dynamic name, object target, out object memberVal) {
            if (target.IsArray()) {
                return ((Array)target).TryGetMember((int)name, out memberVal);
            }

            var member = target.TryGetMemberInfo((string)name);

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

            if (type.IsDictionaryOf<string, object>()) {
                return (Dictionary<string, object>)target;
            }

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

        internal static object Cast(this object target, Type to) {
            var from = target.GetType();
            var key = typeof(Func<,>).MakeGenericType(from, to);

            var @delegate = cache.GetOrAdd(key, () => {
                var param = Expression.Parameter(from, "source");

                return Expression.Lambda(
                            Expression.Convert(param, to),
                            param).Compile();
            });

            return @delegate.DynamicInvoke(target);
        }

        internal static object GetDefaultValue(this Type type) {
            var @delegate = cache.GetOrAdd(type, () => {
                return Expression.Lambda<Func<object>>(
                            Expression.Convert(Expression.Default(type),
                                typeof(object))).Compile();
            });

            return @delegate.DynamicInvoke();
        }

        internal static bool IsAnonymousType(this Type type) {
            return type.IsClass &&
                   type.IsSealed &&
                   type.Attributes.HasFlag(TypeAttributes.NotPublic) &&
                   type.IsDefined(typeof(CompilerGeneratedAttribute), true);
        }

        internal static ConstructorInfo GetConstructor(this object value, out IEnumerable<Type> parameters) {
            var type = value.GetType();
            var ctor = type.GetConstructor(EmptyTypes);

            parameters = new List<Type>();

            if (ctor.IsNotNull()) {
                return ctor;
            }

            ctor = type.GetConstructors()[0];
            parameters = ctor.GetParameters().Select(param => param.ParameterType);

            return ctor;
        }

        internal static Array CreateNewArray(this IList list, int? len = null) {
            return list.GetElementType().CreateNewArray<Array>(len ?? list.Count);
        }

        internal static Array CreateNewArray(this IList list, Array sourceToCopy) {
            var array = list.CreateNewArray(sourceToCopy.Length);

            sourceToCopy.CopyTo(array, 0);

            return array;
        }

        internal static ListType CreateNewArray<ListType>(this Type type, int len) {
            return (ListType)type.MakeArrayType(1).GetConstructors()[0].Invoke(new object[] { len }); ;
        }

        internal static ListType CreateNewList<ListType>(this Type type) {
            return (ListType)typeof(List<>).MakeGenericType(type).GetConstructor(EmptyTypes).Invoke(new object[0]);
        }

        internal static IList CreateNewList(this IEnumerable enumerable, IEnumerable elements = null, Type type = null) {
            type = type ?? enumerable.GetElementType();

            if (elements != null) {
                IList list = null;

                elements.Cast<object>().Select(e => e.Cast(type));
                list = (IList)typeof(List<>).MakeGenericType(type).GetConstructor(EmptyTypes).Invoke(null);
                elements.Cast<object>().ForEach(e => list.Add(e));

                return list;
            }

            return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(EmptyTypes).Invoke(null);
        }

        internal static Type GetElementType(this IEnumerable enumerable) {
            var elementType = typeof(object);
            var enumerableType = enumerable.GetType();

            if (enumerableType.HasElementType) {
                elementType = enumerableType.GetElementType();
            }
            else if (enumerableType.IsGenericType) {
                elementType = enumerableType.GetGenericArguments()[0];
            }
            else {
                Type firstElementType = null;
                var list = enumerable.Cast<object>();
                var allSameType = list.Aggregate(true, (e1, e2) => {
                    firstElementType = firstElementType ?? e2.GetType();

                    return e1 && firstElementType.Equals(e2.GetType());
                });

                if (allSameType) {
                    elementType = firstElementType;
                }
            }

            return elementType;
        }

        internal static Func<object> GetFactory(this object value) {
            IEnumerable<Type> parameters;
            var ctor = value.GetConstructor(out parameters);
            var arguments = parameters.Select(param => Expression.Constant(param.GetDefaultValue(), param));

            return Expression.Lambda<Func<object>>(
                        Expression.Convert(Expression.New(ctor, arguments), typeof(object))).Compile();
        }

        internal static object ToInvokable(this object target) {
            if (target.IsList()) {
                return ((IList)target).Cast<object>().ToArray();
            }

            return target;
        }

        internal static object Invoke(this Delegate target, params object[] arguments) {
            return target.DynamicInvoke(arguments.Pad(target));
        }

        internal static bool Is<TCompareTo>(this object @object) {
            return typeof(TCompareTo).IsAssignableFrom(@object.GetType());
        }

        internal static IComparer ToComparer(this Delegate @delegate, Func<object, object, int> comparator) {
            return new ComparerFactory(comparator);
        }

        internal static bool IsJaggedArray(this object[] arr) {
            return arr.GetType().GetElementType().IsArray;
        }

        internal static object[] ToArgumentsArray(this object value, Type type = null) {
            type = type ?? value.GetType();

            if (type.IsArray) {
                value = value.ToArgumentsArray(type.GetElementType());
            }

            return new[] { value };
        }

        internal static Array ToArray(this IList list) {
            IList arr = list.CreateNewArray(list.Count);

            for (int i = 0; i < list.Count; i++) {
                arr[i] = list[i];
            }

            return (Array)arr;
        }
    }
}
