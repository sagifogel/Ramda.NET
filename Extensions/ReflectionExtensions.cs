using System;
using System.Linq;
using System.Dynamic;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using static Ramda.NET.Lambda;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        private static Type[] EmptyTypes = System.Type.EmptyTypes;
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private static BindingFlags ctorBindingFlags = bindingFlags | BindingFlags.NonPublic;

        internal static TArg CastTo<TArg>(this object arg) {
            if (arg != null && typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        internal static bool IsDelegate(this Type type) {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        internal static bool IsDictionary(this object target) {
            return target.GetType().TypeIsDictionary();
        }

        internal static bool IsExpandoObject(this Type type) {
            return type.Equals(typeof(ExpandoObject));
        }

        internal static bool TypeIsDictionary(this Type type) {
            if (typeof(IDictionary).IsAssignableFrom(type) || type.IsExpandoObject()) {
                return true;
            }

            if (type.IsGenericParameter) {
                var genericTypeDef = type.GetGenericTypeDefinition();

                return genericTypeDef.Equals(typeof(IDictionary<,>));
            }

            return false;
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

        internal static bool IsEnumerable(this object value) {
            return typeof(IEnumerable).IsAssignableFrom(value.GetType());
        }

        internal static bool IsFunction(this object value) {
            return value.GetType().IsDelegate();
        }

        internal static bool IsNull(this object target) {
            if (Equals(target, null)) {
                return true;
            }

            return target.Equals(null);
        }

        internal static bool IsNotNull(this object target) {
            return !target.IsNull();
        }

        internal static object Member(object target, dynamic member) {
            if (member.GetType().Equals(typeof(int)) && target.IsArray()) {
                return ((Array)target).Member((int)member);
            }

            return target.Member((string)member);
        }

        internal static object Member(this object target, string name) {
            var type = target.GetType();

            if (type.TypeIsDictionary()) {
                if (type.Equals(typeof(ExpandoObject))) {
                    var dictionary = (IDictionary<string, object>)target;

                    if (dictionary.ContainsKey(name)) {
                        return dictionary[name];
                    }
                }
                else {
                    var dictionary = target as IDictionary;

                    if (dictionary.Contains(name)) {
                        return dictionary[name];
                    }
                }
            }
            else if (type.IsArray) {
                int index;
                var arr = (IList)target;

                if (int.TryParse(name, out index)) {
                    if (arr.Count > index) {
                        return arr[index];
                    }
                }
            }
            else if (type.IsDelegate() && name.Equals("Length")) {
                return ((Delegate)target).GetFunctionArity();
            }
            else {
                var member = type.TryGetMemberInfoFromType(name);

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
            }

            return R.Null;
        }

        internal static int GetFunctionArity(this Delegate @delegate) {
            if (@delegate.GetType().Equals(typeof(LambdaN))) {
                var methodName = "Invoke";
                var traget = @delegate.Target;
                var fn = traget.GetMemberWhen<FieldInfo>("fn", m => {
                    return m.MemberType == MemberTypes.Field && ((FieldInfo)m).FieldType.IsDelegate();
                });

                if (fn.IsNotNull()) {
                    if (fn.FieldType.Equals(typeof(Delegate))) {
                        methodName = $"Dynamic{methodName}";
                    }

                    return fn.FieldType.GetMethod(methodName).Arity();
                }
            }

            return @delegate.Method.Arity();
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

        internal static bool HasMember(this object target, string name) {
            if (target.IsDictionary()) {
                return ((IDictionary)target).Contains(name);
            }

            return target.Member(name).IsNotNull();
        }

        internal static bool WhenMember(this object target, string name, Func<Type, bool> predicate) {
            var member = target.GetType().TryGetMemberInfoFromType(name);

            return member.IsNotNull() && predicate(member.ReflectedType);
        }

        internal static TMemberInfo GetMemberWhen<TMemberInfo>(this object target, string name, Func<MemberInfo, bool> predicate) where TMemberInfo : MemberInfo {
            var member = target.GetType().TryGetMemberInfoFromType(name);

            if (member.IsNotNull() && predicate(member)) {
                return (TMemberInfo)member;
            }

            return null;
        }

        internal static bool TypeHasMember(this Type type, string name) {
            return type.TryGetMemberInfoFromType(name).IsNotNull();
        }

        internal static IDictionary<string, object> ToMemberDictionary(this object target) {
            var type = target.GetType();

            if (type.IsDictionaryOf<string, object>()) {
                return (IDictionary<string, object>)target;
            }

            if (type.IsArray) {
                return ((IList)target).ToDictionary((item, i) => i.ToString(), (item, i) => item);
            }

            return type.GetProperties(bindingFlags)
                       .Cast<MemberInfo>()
                       .Concat(type.GetFields(bindingFlags))
                       .ToDictionary(member => member.Name, m => target.Member(m.Name));
        }

        internal static IEnumerable<MemberInfo> ToMemberInfos(this object target) {
            return target.GetType().ToMembersInfoFromType();
        }

        internal static IEnumerable<MemberInfo> ToMembersInfoFromType(this Type type) {
            return type.GetProperties(bindingFlags)
                       .Cast<MemberInfo>()
                       .Concat(type.GetFields(bindingFlags));
        }

        internal static string[] Keys(this object target) {
            return target.ToMemberDictionary().Keys.ToArray();
        }

        internal static MemberInfo TryGetMemberInfo(this object target, string name) {
            return target.GetType().TryGetMemberInfoFromType(name);
        }

        internal static MemberInfo TryGetMemberInfoFromType(this Type type, string name) {
            var members = type.GetMember(name, bindingFlags);

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

        internal static bool IsAnonymousType(this object obj) {
            return obj.GetType().IsAnonymousType();
        }

        internal static bool IsAnonymousType(this Type type) {
            return type.IsClass &&
                   type.IsSealed &&
                   type.Attributes.HasFlag(TypeAttributes.NotPublic) &&
                   type.IsDefined(typeof(CompilerGeneratedAttribute), true);
        }

        internal static ConstructorInfo GetConstructor(this object value, out IEnumerable<Type> parameters) {
            var type = value.GetType();
            var ctor = type.GetConstructor(ctorBindingFlags, null, EmptyTypes, null);

            parameters = new List<Type>();

            if (ctor.IsNotNull()) {
                return ctor;
            }

            ctor = type.GetConstructors(ctorBindingFlags)[0];
            parameters = ctor.GetParameters().Select(param => param.ParameterType);

            return ctor;
        }

        internal static Func<object> GetFactory(this object value) {
            IEnumerable<Type> parameters;
            var ctor = value.GetConstructor(out parameters);
            var arguments = parameters.Select(param => Expression.Constant(param.GetDefaultValue(), param));

            return Expression.Lambda<Func<object>>(
                        Expression.Convert(Expression.New(ctor, arguments), typeof(object))).Compile();
        }

        internal static Delegate GetFactory(this Type type, int arity) {
            Type delegateType;
            Type[] parameterTypes;
            IEnumerable<ParameterExpression> parameters;
            var ctor = type.GetConstructors(ctorBindingFlags)
                           .FirstOrDefault(c => c.GetParameters().Length == arity);

            if (ctor.IsNull()) {
                throw new ArgumentOutOfRangeException($"Constructor does not have {arity} arguments");
            }

            parameterTypes = ctor.GetParameters().Select(p => p.ParameterType).ToArray();
            parameters = parameterTypes.Select(param => Expression.Parameter(param)).ToArray();
            delegateType = Expression.GetFuncType(parameterTypes.Concat(new[] { type }).ToArray());

            return Expression.Lambda(delegateType, Expression.New(ctor, parameters), parameters).Compile();
        }

        internal static object Invoke(this Delegate target, params object[] arguments) {
            return target.DynamicInvoke(arguments.Pad(target));
        }

        internal static object InvokeWithArray(this Delegate target, object[] arguments) {
            return target.DynamicInvoke(arguments.Pad(target));
        }

        internal static bool Is<TCompareTo>(this object @object) {
            return typeof(TCompareTo).IsAssignableFrom(@object.GetType());
        }

        internal static IComparer ToComparer(this Delegate @delegate, Func<object, object, int> comparator) {
            return new ComparerFactory(comparator);
        }

#if !NET_4_5

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider type, bool inherit = true) where TAttribute : Attribute {
            return type.GetCustomAttributes(typeof(TAttribute), inherit)
                       .Cast<TAttribute>();
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this ICustomAttributeProvider type, bool inherit = true) where TAttribute : Attribute {
            return type.GetCustomAttributes<TAttribute>(inherit).FirstOrDefault();
        }

#endif

        internal static bool IsDefined<TAttribute>(this ICustomAttributeProvider attributeProvider, out TAttribute attribute, bool inherit = true) where TAttribute : Attribute {
            attribute = default(TAttribute);

            if (attributeProvider.IsDefined(typeof(TAttribute), inherit)) {
                attribute = attributeProvider.GetCustomAttribute<TAttribute>(inherit);
                return true;
            }

            return false;
        }

        internal static bool HasAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit = true) where TAttribute : Attribute {
            return attributeProvider.GetCustomAttributes<TAttribute>(inherit).FirstOrDefault().IsNotNull();
        }
    }
}
