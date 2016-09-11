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
            objectN.ForEach(source => {
                var lambdaType = Expression.GetActionType(typeof(ExpandoObject), source.GetType());

                var @delegate = cache.GetOrAdd(lambdaType, () => {
                    var targetParameter = Expression.Parameter(typeofDictionary, "target");
                    var sourceParameter = Expression.Parameter(source.GetType(), "source");
                    var lambda = Expression.Lambda(lambdaType,
                                        Expression.Block(GetAssignExpressions(source, targetParameter, sourceParameter)),
                                        targetParameter,
                                        sourceParameter);

                    return lambda.Compile();
                });

                @delegate.DynamicInvoke(target, source);
            });

            return target;
        }

        private static IEnumerable<Expression> GetAssignExpressions(object source, ParameterExpression target, ParameterExpression sourceParameter) {
            foreach (var member in source.ToMemberInfos()) {
                yield return Expression.Assign(
                                Expression.Property(target, "Item", Expression.Constant(member.Name)),
                                Expression.Convert(Expression.PropertyOrField(sourceParameter, member.Name), typeofObject));
            }
        }

        private static Expression GetAssignExpression(object source, ParameterExpression target, ParameterExpression sourceParameter) {
            var member = source.ToMemberInfos().Single(m => m.Name.Equals("b"));

            return Expression.Assign(
                            Expression.Property(target, "Item", Expression.Constant(member.Name)),
                            Expression.PropertyOrField(sourceParameter, member.Name));
        }
    }
}
