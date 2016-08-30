using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class ShallowCloner
    {
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();

        internal static object CloneAndAssignValue(string prop, object propValue, object obj) {
            object target = null;
            var type = obj.GetType();

            if (type.IsAnonymousType()) {
                target = AnonymousTypeCloneAndAssignValue(prop, (_) => propValue, type, obj);
            }
            else {
                target = WellKnownCloneAndAssignValue(prop, obj, propValue);
            }

            return target;
        }

        internal static object CloneAndAssignDefaultValue(string prop, object obj) {
            object target = null;
            var type = obj.GetType();

            if (type.IsAnonymousType()) {
                target = AnonymousTypeCloneAndAssignValue(prop, (paramType) => paramType.GetDefaultValue(), type, obj);
            }
            else {
                target = WellKnownCloneAndAssignValue(prop, obj, null);
            }

            return target;
        }

        private static object AnonymousTypeCloneAndAssignValue(string prop, Func<Type, object> propValueFactory, Type type, object obj) {
            ConstructorInfo ctor = null;
            ParameterInfo[] parameters = null;
            var arguments = new List<object>();

            ctor = type.GetConstructors()[0];
            parameters = ctor.GetParameters();

            arguments.AddRange(parameters.Select(param => {
                if (param.Name.Equals(prop)) {
                    return propValueFactory(param.ParameterType);
                }

                return obj.Member(param.Name).Clone();
            }));

            return ctor.Invoke(arguments.ToArray());
        }

        private static object WellKnownCloneAndAssignValue(string prop, object obj, object value) {
            var target = Clone(obj);
            MemberInfo member = obj.TryGetMember(prop);

            switch (member.MemberType) {
                case MemberTypes.Field:
                    var property = (PropertyInfo)member;

                    if (property.CanWrite) {
                        property.SetValue(target, value);
                    }

                    break;
                case MemberTypes.Property:
                    var field = (FieldInfo)member;

                    if (!field.IsInitOnly) {
                        field.SetValue(target, value);
                    }

                    break;
            }

            return target;
        }

        private static object Clone(this object source) {
            Delegate @delegate = null;
            var type = source.GetType();

            if (!cache.TryGetValue(type, out @delegate)) {
                var parameter = Expression.Parameter(type);
                var clone = Expression.Call(parameter, type.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic));
                @delegate = Expression.Lambda(Expression.GetFuncType(new[] { type, type }), Expression.Convert(clone, type), parameter).Compile();
                cache.Add(type, @delegate);
            }

            return @delegate.DynamicInvoke(source);
        }

        private static object GetDefaultValue(this Type type) {
            Delegate @delegate;

            if (!cache.TryGetValue(type, out @delegate)) {
                @delegate = Expression.Lambda<Func<object>>(
                                Expression.Convert(Expression.Default(type),
                                                    typeof(object))).Compile();
            }

            return ((Func<object>)@delegate)();
        }
    }
}
