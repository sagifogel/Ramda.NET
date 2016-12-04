using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Dynamic;

namespace Ramda.NET
{
    internal static class ShallowCloner
    {
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();

        internal static object Clone(this object source) {
            var type = source.GetType();

            var @delegate = cache.GetOrAdd(type, () => {
                var parameter = Expression.Parameter(type);
                var clone = Expression.Call(parameter, type.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic));
                var lambda = Expression.Lambda(
                                    Expression.GetFuncType(new[] { type, type }),
                                    Expression.Convert(clone, type), parameter);

                return lambda.Compile();
            });

            return @delegate.DynamicInvoke(source);
        }

        internal static object CloneAndAssignValue(string prop, object propValue, object obj) {
            object target = null;
            var type = obj.GetType();

            if (type.IsAnonymousType()) {
                target = AnonymousTypeCloneAndAssignValue(prop, propValue, type, obj);
            }
            else {
                target = WellKnownTypeCloneAndAssignValue(prop, obj, propValue);
            }

            return target;
        }

        internal static object CloneAndAssignDefaultValue(string prop, object obj) {
            object target = null;
            var type = obj.GetType();

            if (type.IsAnonymousType()) {
                var member = obj.Member(prop);

                target = AnonymousTypeCloneAndAssignValue(prop, member.GetType().GetDefaultValue(), type, obj);
            }
            else {
                target = WellKnownTypeCloneAndAssignValue(prop, obj, null);
            }

            return target;
        }

        private static object AnonymousTypeCloneAndAssignValue(string prop, object propValue, Type type, object obj) {
            var propHasBeenSet = false;
            bool shouldCreateExpando = false;
            var arguments = new List<object>();
            var ctor = type.GetConstructors()[0];
            var tuples = ctor.GetParameters().Select((param, i) => {
                object result = null;
                var paramType = param.ParameterType;

                if (param.Name.Equals(prop)) {
                    propHasBeenSet = true;
                    result = propValue;
                }
                else {
                    result = obj.Member(param.Name);
                }

                shouldCreateExpando |= !paramType.Equals(result.GetType());

                return new {
                    Value = result,
                    Name = param.Name
                };
            }).ToList();

            if (shouldCreateExpando || !propHasBeenSet) {
                IDictionary<string, object> expando = new ExpandoObject();

                tuples.ForEach(tuple => expando[tuple.Name] = tuple.Value);
                expando[prop] = propValue;

                return expando;
            }

            arguments.AddRange(tuples.Select(tuple => tuple.Value));

            return ctor.Invoke(arguments.ToArray());
        }

        private static object WellKnownTypeCloneAndAssignValue(string prop, object obj, object value) {
            var target = obj.Clone();
            MemberInfo member = obj.TryGetMemberInfo(prop);

            switch (member.MemberType) {
                case MemberTypes.Field:
                    var property = (PropertyInfo)member;

                    if (property.CanWrite) {
                        property.SetValue(target, value, null);
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
    }
}
