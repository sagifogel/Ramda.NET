using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ramda.NET
{
    public static class MemberwiseComparer
    {
        private static Dictionary<string, Delegate> cache = new Dictionary<string, Delegate>();

        public static bool Compare(object x, object y) {
            var comparer = GetComparer(x, y);

            return (bool)comparer.DynamicInvoke(x, y);
        }

        private static Delegate GetComparer(object first, object second) {
            var firstType = first.GetType();
            var secondType = second.GetType();
            var key = $"{firstType.AssemblyQualifiedName}:{secondType.AssemblyQualifiedName}";

            return cache.GetOrAdd(key, () => {
                Expression expression;
                Expression body = null;
                var xMembers = firstType.ToMembersInfoFromType();
                var yMembers = secondType.ToMembersInfoFromType();
                var members = xMembers.Concat(yMembers).Distinct(memberInfo => memberInfo.Name);
                var x = Expression.Parameter(firstType, "x");
                var y = Expression.Parameter(secondType, "y");

                foreach (var member in members) {
                    if (TryAddExpression(body, x, y, member.Name, out expression)) {
                        body = expression;
                    }
                    else {
                        body = Expression.Constant(false);
                        break;
                    }
                }

                return Expression.Lambda(body ?? Expression.Constant(true), x, y).Compile();
            });
        }

        private static bool TryAddExpression(Expression body, Expression x, Expression y, string propertyOrField, out Expression expression) {
            expression = null;

            if (x.Type.TypeHasMember(propertyOrField) && y.Type.TypeHasMember(propertyOrField)) {
                var equals = Expression.Equal(Expression.PropertyOrField(x, propertyOrField), Expression.PropertyOrField(y, propertyOrField));

                expression = body.IsNull() ? equals : Expression.AndAlso(body, equals);
                return true;
            }

            return false;
        }
    }
}
