using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Dynamic;
using System.Collections;
using System.ComponentModel;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static class ShallowCloner
    {
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();

        internal static object Clone(this object source) {
            var type = source.GetType();

            if (type.IsAnonymousType()) {
                return source.ToExpando();
            }
            else {
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
        }

        internal static object CloneAndAssignValue(dynamic prop, object propValue, object obj) {
            object target = null;
            var type = obj.GetType();

            if (type.IsAnonymousType()) {
                target = AnonymousTypeCloneAndAssignValue(prop, propValue, type, obj);
            }
            else {
                MemberInfo memberInfo = Reflection.TryGetMemberInfoFromType(type, prop);

                if (memberInfo != null && memberInfo.GetUnderlyingType().IsAssignableFrom(propValue.GetType())) {
                    target = WellKnownTypeCloneAndAssignValue(prop, obj, propValue);
                }
                else {
                    IDictionary<string, object> expando = Reflection.ToExpando(obj);

                    expando[prop] = propValue;
                    target = expando;
                }
            }

            return target;
        }

        internal static object CloneAndOmitValue(string prop, object obj) {
            IDictionary<string, object> expando = obj.ToExpando();

            expando.Remove(prop);

            return expando as ExpandoObject;
        }

        private static object AnonymousTypeCloneAndAssignValue(object prop, object propValue, Type type, object obj) {
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
                expando[prop.ToString()] = propValue;

                return expando;
            }

            arguments.AddRange(tuples.Select(tuple => tuple.Value));

            return ctor.Invoke(arguments.ToArray());
        }

        private static object WellKnownTypeCloneAndAssignValue(object prop, object obj, object value) {
            var target = obj.Clone();

            if (target.IsList() && prop.GetType().Equals(typeof(int))) {
                var index = (int)prop;
                var list = target as IList;

                list[index] = value;
            }
            else if (target.IsExpandoObject()) {
                var expando = target as IDictionary<string, object>;

                expando[prop.ToString()] = value;
            }
            else {
                var member = obj.TryGetMemberInfo(prop.ToString());

                if (member.IsNotNull()) {
                    switch (member.MemberType) {
                        case MemberTypes.Property:
                            var property = (PropertyInfo)member;

                            if (property.CanWrite) {
                                property.SetValue(target, value, null);
                            }

                            break;
                        case MemberTypes.Field:
                            var field = (FieldInfo)member;

                            if (!field.IsInitOnly) {
                                field.SetValue(target, value);
                            }

                            break;
                    }
                }
            }

            return target;
        }
    }
}
