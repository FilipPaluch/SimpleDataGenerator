using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataGenerator.Core.Extensions
{
    public static class PropertyExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(
                this Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);
            MemberExpression member = null;

            if (propertyLambda.Body is MemberExpression) member = (MemberExpression)propertyLambda.Body;

            if (propertyLambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)propertyLambda.Body;
                member = (MemberExpression)unaryExpression.Operand;
            }
            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;

        }

        public static MemberExpression GetMemberInfo(this Expression method)
        {
            LambdaExpression lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

    }
}
