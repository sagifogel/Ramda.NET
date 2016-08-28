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
            var target = Clone(obj);
            var property = target.GetType().GetProperty(prop, ReflectionExtensions.bindingFlags);

            property.SetValue(target, propValue);

            return target;
        }

        internal static object CloneAndAssignDefaultValue(string prop, object obj) {
            var target = Clone(obj);
            var property = target.GetType().GetProperty(prop, ReflectionExtensions.bindingFlags);

            property.SetValue(target, null);

            return target;
        }

        internal static object GetDefaultValue(this Type type) {
            Delegate @delegate;

            if (!cache.TryGetValue(type, out @delegate)) {
                @delegate = Expression.Lambda<Func<object>>(
                                Expression.Convert(Expression.Default(type),
                                                    typeof(object))).Compile();
            }

            return ((Func<object>)@delegate)();
        }

        internal static object Clone(object source) {
            Delegate @delegate = null;
            var type = source.GetType();

            if (!cache.TryGetValue(type, out @delegate)) {
                var list = new List<object>();
                MemberInitExpression memberInit = null;
                var parameter = Expression.Parameter(type);
                var ctor = source.GetType().GetConstructor(list);
                var newExpression = Expression.New(ctor, list.Select(arg => Expression.Constant(arg, arg.GetType())));
                IEnumerable<MemberBinding> memberBindings = null;

                memberBindings = source.GetType()
                                       .GetMembers(ReflectionExtensions.bindingFlags)
                                       .Where(member => member.MemberType == MemberTypes.Field || (member.MemberType == MemberTypes.Property && ((PropertyInfo)member).CanWrite))
                                       .Select(field => Expression.Bind(field, Expression.Constant(source.Member(field.Name))));
                
                memberInit = Expression.MemberInit(newExpression, memberBindings);
                @delegate = Expression.Lambda<Func<object, object>>(memberInit, parameter).Compile();
                cache.Add(type, @delegate);
            }

            return @delegate.DynamicInvoke(source);
        }

        private static ConstructorInfo GetConstructor(this Type type, List<object> list) {
            var ctor = type.GetConstructor(Type.EmptyTypes);

            if (ctor != null) {
                return ctor;
            }

            ctor = type.GetConstructors()[0];
            ctor.GetParameters().ForEach(param => list.Add(param.ParameterType.GetDefaultValue()));

            return ctor;
        }
    }
}
