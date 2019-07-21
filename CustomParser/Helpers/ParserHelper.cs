using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CustomParser.Helpers
{
   public static class ParserHelper
   {
      public static string GetPropertyNameFromExpression<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
      {
         MemberInfo member = GetMemberExpression(expression).Member;

         return member.Name;
      }

      private static MemberExpression GetMemberExpression<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
      {
         MemberExpression memberExpression = null;
         switch (expression.Body.NodeType)
         {
            case ExpressionType.Convert:
               UnaryExpression body = (UnaryExpression) expression.Body;
               memberExpression = body.Operand as MemberExpression;
               break;
            case ExpressionType.MemberAccess:
               memberExpression = expression.Body as MemberExpression;
               break;
         }

         if (memberExpression == null) throw new ArgumentException("Not a member access", nameof(expression));

         return memberExpression;
      }

      private static PropertyInfo GetProperty<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
      {
         MemberInfo member = GetMemberExpression(expression).Member;
         PropertyInfo property = member as PropertyInfo;

         if (property != null) return property;
         string message = $"Member with Name '{member.Name}' is not a property.";

         throw new InvalidOperationException(message);
      }

      public static Action<TEntity, TProperty> CreateSetter<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
      {
         PropertyInfo propertyInfo = GetProperty(property);

         ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");
         ParameterExpression parameter = Expression.Parameter(typeof(TProperty), "param");

         MethodInfo setMethod = propertyInfo.GetSetMethod();
         if (setMethod == null) throw new InvalidOperationException($"Unable to map to property '{property.Body}' because it does not contain a setter.");
         return Expression.Lambda<Action<TEntity, TProperty>>(
            Expression.Call(instance, setMethod, parameter), instance, parameter).Compile();
      }
   }
}