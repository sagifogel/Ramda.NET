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
        internal static BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
        private static BindingFlags ctorBindingFlags = bindingFlags | BindingFlags.NonPublic;
        private static ISet<Type> primitives = new HashSet<Type> {  typeof(Enum), typeof(string), typeof(char),
                                                                    typeof(Guid), typeof(bool), typeof(byte),
                                                                    typeof(short), typeof(int), typeof(long),
                                                                    typeof(float), typeof(double), typeof(decimal),
                                                                    typeof(sbyte), typeof(ushort), typeof(uint), typeof(ulong),
                                                                    typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan),
                          };
        internal static IList Arity(params object[] arguments) {
            IList result;

            if (arguments.IsNull() || arguments.Length == 0) {
                return emptyArray;
            }

            int i = arguments.Length - 1;

            while (i >= 0) {
                if (!Equals(arguments[i], null)) {
                    break;
                }

                i--;
            }

            result = arguments.Slice(0, ++i);

            return result.Count == 0 ? emptyArray : result;
        }

        internal static bool TypeIsDelegate(this Type type) {
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

        internal static bool TypeIsSet(this Type type) {
            if (type.IsGenericType) {
                var genericTypeDef = type.GetGenericTypeDefinition();

                return genericTypeDef.Equals(typeof(HashSet<>));
            }

            return false;
        }

        internal static bool TypeIsDictionaryOf<TKey, TValue>(this Type type) {
            return typeof(IDictionary<TKey, TValue>).IsAssignableFrom(type);
        }

        internal static bool IsArray(this object value) {
            return value.GetType().TypeIsArray();
        }

        internal static bool TypeIsArray(this Type type) {
            return typeof(Array).IsAssignableFrom(type);
        }

        internal static bool IsList(this object value) {
            return typeof(IList).IsAssignableFrom(value.GetType());
        }

        internal static bool IsEnumerable(this object value) {
            return typeof(IEnumerable).IsAssignableFrom(value.GetType());
        }

        internal static bool IsFunction(this object value) {
            return value.GetType().TypeIsFunction();
        }

        internal static bool TypeIsFunction(this Type type) {
            return type.TypeIsDynamicDelegate() || type.TypeIsDelegate();
        }

        internal static bool TypeIsDynamicDelegate(this Type type) {
            return typeof(DynamicDelegate).IsAssignableFrom(type);
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

                return R.@null;
            }

            return target.Member((string)member);
        }

        internal static object MemberOr(object target, dynamic member, Func<object> orFn) {
            object result = Member(target, member);

            return result.IsNotNull() ? result : orFn();
        }

        internal static object Member(this object target, object key, int length = 0, bool @private = false) {
            var type = target.GetType();
            var name = key.ToString();
            var isLength = name.Equals("Length");

            if (type.TypeIsDictionary()) {
                var result = target.DictionaryMember(key, type);

                if (result != null) {
                    return result;
                }
            }
            else if (type.TypeIsDelegate() && isLength) {
                return FunctionArity(target);
            }
            else if (!isLength) {
                var arr = target as IList;

                if (arr != null) {
                    int index;

                    if (int.TryParse(name, out index)) {
                        if (arr.Count > index) {
                            return arr[index];
                        }

                        return R.@null;
                    }
                }
            }

            var member = type.TryGetMemberInfoFromType(name, length, @private);

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

            return R.@null;
        }

        internal static object DictionaryMember(this object target, object key, Type type = null) {
            type = type ?? target.GetType();

            if (type.TypeIsDictionaryOf<string, object>()) {
                var name = key.ToString();
                var dictionary = (IDictionary<string, object>)target;

                if (dictionary.ContainsKey(name)) {
                    return dictionary[name];
                }
            }
            else {
                var dictionary = target as IDictionary;

                if (dictionary.Contains(key)) {
                    return dictionary[key];
                }
            }

            return null;
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
            return (memberVal = Member(target, name)).IsNotNull();
        }

        internal static TConvert MemberWhere<TConvert>(this object target, string name, Func<TConvert, bool> predicate) {
            object value;

            if (TryGetMember((dynamic)name, target, out value)) {
                TConvert converted = (TConvert)value;

                if (predicate(converted)) {
                    return converted;
                }
            }

            return default(TConvert);
        }

        internal static TMemberInfo GetMemberWhen<TMemberInfo>(this object target, string name, Func<MemberInfo, bool> predicate) where TMemberInfo : MemberInfo {
            var member = target.GetType().TryGetMemberInfoFromType(name);

            if (member.IsNotNull() && predicate(member)) {
                return (TMemberInfo)member;
            }

            return null;
        }

        internal static IDictionary<string, object> ToMemberDictionary(this object target) {
            var type = target.GetType();
            IDictionary dictionary = null;

            if (type.TypeIsDictionaryOf<string, object>()) {
                return (IDictionary<string, object>)target;
            }

            dictionary = target as IDictionary;

            if (dictionary != null) {
                return dictionary.Keys.Cast<string>().ToDictionary(k => k, k => dictionary[k]);
            }

            if (type.TypeIsArray()) {
                return ((IList)target).ToDictionary((item, i) => i.ToString(), (item, i) => item);
            }

            return type.GetProperties(bindingFlags)
                       .Cast<MemberInfo>()
                       .Concat(type.GetFields(bindingFlags))
                       .ToDictionary(member => member.Name, m => target.Member(m.Name));
        }

        internal static string[] Keys(this object target) {
            return target.ToMemberDictionary().Keys.ToArray();
        }

        internal static MemberInfo TryGetMemberInfo(this object target, string name) {
            return target.GetType().TryGetMemberInfoFromType(name);
        }

        internal static MemberInfo TryGetMemberInfoFromType(this Type type, string name, int length = 0, bool @private = false) {
            MemberInfo[] members;
            MemberInfo[] ownMembers;
            var flags = bindingFlags;

            if (@private) {
                flags |= BindingFlags.NonPublic;
            }

            members = type.GetMember(name, flags);

            if (members.Length == 1) {
                return members[0];
            }

            ownMembers = members.Where(m => {
                if (m.MemberType == MemberTypes.Method) {
                    return ((MethodInfo)m).IsOverriden(type);
                }

                return true;
            }).ToArray();

            if (ownMembers.Length == 1) {
                return ownMembers[0];
            }

            return members.Where(m => m.MemberType == MemberTypes.Method)
                          .Cast<MethodInfo>().SingleOrDefault(m => m.GetParameters().Length == length);
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

        internal static object Invoke(this Delegate target, object[] arguments) {
            if (target.Arity() == 1) {
                var param = target.Method.GetParameters()[0].ParameterType;

                if (param.TypeIsArray() && (arguments.Length != 1 || arguments.Length == 1 && (arguments[0].IsNotNull() && !arguments[0].IsArray()))) {
                    arguments = new[] { arguments };
                }
            }

            try {
                return target.DynamicInvoke(arguments.Clip(target));
            }
            catch (TargetInvocationException ex) {
                throw ex.InnerException;
            }
        }

        internal static object DynamicInvoke(dynamic target, object[] arguments = null) {
            var args = Arguments(arguments) ?? emptyArray;

            return DynamicInvoke(target, args, args.Count);
        }

        internal static object DynamicDirectInvoke(dynamic target, object[] arguments = null) {
            return DynamicInvoke(target, arguments, arguments.Length);
        }

        internal static object InvokeNative(this Delegate @delegate, object[] arguments) {
            var args = arguments.Unwrap(@delegate);

            return DynamicInvoke(Delegate(@delegate), args, args.Length);
        }

        internal static object DynamicInvoke(dynamic invoke, IList args, int length) {
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

        internal static IComparer ToComparer(this DynamicDelegate @delegate, Func<object, object, int> comparator) {
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
                if (string.Empty.Equals(obj)) {
                    obj = 0;
                }

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

        internal static bool IsOverriden(this Delegate @delegate, Type type) {
            return @delegate.Method.IsOverriden(type);
        }

        internal static bool IsOverriden(this MethodInfo methodInfo, Type type) {
            return methodInfo.DeclaringType.Equals(type);
        }

        internal static bool TypeIsPrimitive(this Type type) {
            return primitives.Contains(type);
        }

        internal static bool IsPrimitive(this object @object) {
            return @object.GetType().TypeIsPrimitive();
        }

        internal static bool ContainsKey(this IDictionary dictionary, object key) {
            return dictionary.ContainsKey(new[] { key });
        }

        internal static bool ContainsKeys(this IDictionary dictionary, object[] keys) {
            var contains = GetDictionaryContains(dictionary);

            return keys.All(contains);
        }

        private static Func<object, bool> GetDictionaryContains(IDictionary dictionary) {
            var dictionaryWithStringKey = dictionary as IDictionary<string, object>;

            if (dictionaryWithStringKey != null) {
                return key => dictionaryWithStringKey.ContainsKey(key.ToString());
            }

            return key => dictionary.Contains(key);
        }

        internal static bool Has(this object obj, string prop) {
            IList list = null;
            MemberInfo member = null;

            if (obj.IsDictionary()) {
                var dictionary = obj as IDictionary;
                IDictionary<string, object> expandoDictionary;

                if (dictionary.IsNotNull()) {
                    return dictionary.Contains(prop);
                }

                expandoDictionary = obj as IDictionary<string, object>;

                if (expandoDictionary.IsNotNull()) {
                    return expandoDictionary.ContainsKey(prop);
                }
            }

            list = obj as IList;

            if (list != null) {
                int index;

                if (int.TryParse(prop, out index)) {
                    return index <= list.Count - 1;
                }
            }

            member = obj.TryGetMemberInfo(prop);

            if (member != null) {
                return member.DeclaringType.Equals(obj.GetType());
            }

            return false;
        }

        internal static bool IsObjectArray(this Type type) {
            return type.HasElementType ? type.GetElementType().Equals(typeof(object)) :
                   typeof(IList).IsAssignableFrom(type);
        }

        internal static bool IsInteger(this object target) {
            return target.GetType().Equals(typeof(int));
        }
    }
}
