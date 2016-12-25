using System;
using System.Linq;
using Sys = System;
using System.Dynamic;
using System.Reflection;
using System.Collections;
using System.Linq.Expressions;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ramda.NET
{
    internal static class ReflectionExtensions
    {
        private static object[] emptyArray = new object[0];
        private static Type[] EmptyTypes = System.Type.EmptyTypes;
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();
        private static BindingFlags ctorBindingFlags = bindingFlags | BindingFlags.NonPublic;
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

        internal static object[] Arity(params object[] arguments) {
            object[] result;

            if (arguments.IsNull() || arguments.Length == 0) {
                return null;
            }

            int i = arguments.Length - 1;

            while (i >= 0) {
                if (!Equals(arguments[i], null)) {
                    break;
                }

                i--;
            }

            result = (object[])arguments.Slice(0, ++i);

            return result.Length == 0 ? null : result;
        }

        internal static bool IsDelegate(this Type type) {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        internal static bool IsDictionary(this object target) {
            return target.GetType().TypeIsDictionary();
        }
        internal static bool IsExpandoObject(this object target) {
            return target.GetType().TypeIsExpandoObject();
        }

        internal static bool TypeIsExpandoObject(this Type type) {
            return type.Equals(typeof(ExpandoObject));
        }

        internal static bool TypeIsDictionary(this Type type) {
            if (typeof(IDictionary).IsAssignableFrom(type) || type.TypeIsExpandoObject()) {
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
            return typeof(DynamicDelegate).IsAssignableFrom(value.GetType()) || value.GetType().IsDelegate();
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
            if (member.GetType().Equals(typeof(int))) {
                if (target.IsArray()) {
                    return ((Array)target).Member((int)member);
                }

                return R.Null;
            }

            return target.Member((string)member);
        }

        internal static object MemberOr(object target, dynamic member, Func<object> orFn) {
            object result = Member(target, member);

            return result.IsNotNull() ? result : orFn();
        }

        internal static object Member(this object target, string name, int length = 0) {
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
                return FunctionArity(target);
            }
            else {
                var member = type.TryGetMemberInfoFromType(name, length);

                if (member.IsNotNull()) {
                    switch (member.MemberType) {
                        case MemberTypes.Field:
                            return ((FieldInfo)member).GetValue(target);
                        case MemberTypes.Property:
                            return ((PropertyInfo)member).GetValue(target, null);
                        case MemberTypes.Method:
                            return ((MethodInfo)member).CreateDelegate(target);
                        default:
                            break;
                    }
                }
            }

            return R.Null;
        }

        internal static int FunctionArity(dynamic @delegate) {
            var dynamicDelegate = @delegate as DynamicDelegate;

            if (dynamicDelegate.IsNotNull()) {
                return dynamicDelegate.Length;
            }

            return ((Delegate)@delegate).Method.Arity();
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
                    case MemberTypes.Method:
                        memberVal = ((MethodInfo)member).CreateDelegate(target);
                        return true;
                }
            }

            return false;
        }

        internal static bool HasMemberWhere(this object target, string name, Func<object, bool> predicate) {
            object value;

            if (TryGetMember((dynamic)name, target, out value)) {
                return predicate(value);
            }

            return false;
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

        internal static MemberInfo TryGetMemberInfoFromType(this Type type, string name, int length = 0) {
            var members = type.GetMember(name, bindingFlags);

            if (members.Length == 1) {
                return members[0];
            }

            return members.Cast<MethodInfo>().SingleOrDefault(m => m.GetParameters().Length == length);
        }

        internal static object Cast(this object target, Type to) {
            if (target is IConvertible) {
                return Convert.ChangeType(target, to);
            }

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
            delegateType = Expression.GetFuncType(parameterTypes.Concat<Type>(new[] { type }).ToArray());

            return Expression.Lambda(delegateType, Expression.New(ctor, parameters), parameters).Compile();
        }

        internal static object Invoke(this Delegate target, params object[] arguments) {
            if (target.Arity() == 1) {
                var param = target.Method.GetParameters()[0];

                if (param.ParameterType.IsArray && (arguments.Length != 1 || arguments.Length == 1 && (arguments[0].IsNotNull() && !arguments[0].IsArray()))) {
                    arguments = new object[1] { arguments };
                }
            }

            return target.DynamicInvoke(arguments.Clip(target));
        }

        internal static object DynamicInvoke(dynamic target, object[] arguments = null) {
            var args = Arguments(arguments) ?? emptyArray;

            return DynamicInvoke(target, args, args.Length);
        }

        internal static object DynamicInvoke(dynamic invoke, object[] arguments, int length) {
            var args = Arguments(arguments);

            switch (length) {
                case 0:
                    return invoke();
                case 1:
                    return invoke(args[0]);
                case 2:
                    return invoke(args[0], args[1]);
                case 3:
                    return invoke(args[0], args[1], args[2]);
                case 4:
                    return invoke(args[0], args[1], args[2], args[3]);
                case 5:
                    return invoke(args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return invoke(args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return invoke(args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return invoke(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return invoke(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                case 10:
                    return invoke(args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);
                default:
                    throw new ArgumentOutOfRangeException("Argument length must be a non-negative integer no greater than ten");
            }
        }

        internal static IComparer ToComparer(this Delegate @delegate, Func<object, object, int> comparator) {
            return new ComparerFactory(comparator);
        }

        internal static Delegate CreateDelegate(this MethodInfo methodInfo, object target) {
            Func<Type[], Type> getType;
            bool isAction = methodInfo.ReturnType.Equals((typeof(void)));
            var types = methodInfo.GetParameters().Select(p => p.ParameterType);

            if (isAction) {
                getType = Expression.GetActionType;
            }
            else {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] { methodInfo.ReturnType });
            }

            if (methodInfo.IsStatic) {
                return Sys.Delegate.CreateDelegate(getType(types.ToArray()), methodInfo);
            }

            return Sys.Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }

        internal static dynamic ToNumber(this object obj) {
            var type = obj.GetType();

            if (type.Equals(typeof(string))) {
                return Convert.ToDouble(obj);
            }

            if (type.Equals(typeof(bool))) {
                return Convert.ToInt32(obj);
            }

            if (obj.IsNull()) {
                return 0;
            }

            return (dynamic)obj;
        }
    }
}
