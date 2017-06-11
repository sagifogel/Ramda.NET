using System;
using System.Linq;
using System.Dynamic;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Reflection;

namespace Ramda.NET
{
    internal static class ObjectAssigner
    {
        private static readonly Type typeofObject = typeof(object);
        private static readonly Type typeofFieldInfo = typeof(FieldInfo);
        private delegate ExpandoObject AssignDelegate(ExpandoObject target, object source);
        private static readonly Type typeofDictionary = typeof(IDictionary<string, object>);
        private static Dictionary<Type, Delegate> cache = new Dictionary<Type, Delegate>();
        private static readonly MethodInfo setValueMethod = typeofFieldInfo.GetMethod("SetValue", new[] { typeofObject, typeofObject });

        internal static ExpandoObject Assign(ExpandoObject target, params object[] objectN) {
            objectN.ForEach<object>(source => {
                Delegate @delegate = null;
                var sourceType = source.GetType();
                var lambdaType = Expression.GetActionType(sourceType, typeof(ExpandoObject));

                if (sourceType.TypeIsExpandoObject()) {
                    @delegate = CompileLambdaExpression(source, lambdaType, sourceType);
                }
                else {
                    @delegate = cache.GetOrAdd(lambdaType, () => CompileLambdaExpression(source, lambdaType, sourceType));
                }

                @delegate.DynamicInvoke(source, target);
            });

            return target;
        }

        private static Delegate CompileLambdaExpression(object source, Type lambdaType, Type sourceType) {
            var sourceParameter = Expression.Parameter(sourceType, "source");
            var targetParameter = Expression.Parameter(typeofDictionary, "target");
            var lambda = Expression.Lambda(lambdaType,
                                Expression.Block(GetAssignExpressions(source, targetParameter, sourceParameter)),
                                sourceParameter,
                                targetParameter);

            return lambda.Compile();
        }

        private static IEnumerable<Expression> GetAssignExpressions(object source, ParameterExpression targetParameter, ParameterExpression sourceParameter) {
            var assignExpression = GetAssignExpression(source, sourceParameter);
            var members = source.ToMemberDictionary();
            var emptyExpression = Expression.Empty();

            if (members.Count > 0) {
                foreach (var pair in members) {
                    if (source.Has(pair.Key)) {
                        yield return Expression.Assign(
                                        Expression.Property(targetParameter, "Item", Expression.Constant(pair.Key)),
                                        assignExpression(pair.Key));
                    }
                    else {
                        yield return emptyExpression;
                    }
                }
            }
            else {
                yield return emptyExpression;
            }
        }

        private static Func<string, Expression> GetAssignExpression(object target, ParameterExpression sourceParameter) {
            if (target.IsExpandoObject()) {
                return (string key) => Expression.Convert(Expression.Property(Expression.Convert(sourceParameter, typeofDictionary), "Item", Expression.Constant(key)), typeofObject);
            }

            return (string key) => Expression.Convert(Expression.PropertyOrField(sourceParameter, key), typeofObject);
        }
    }
}
