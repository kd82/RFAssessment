using System;
using System.Linq.Expressions;
using CustomParser.Helpers;
using CustomParser.TypeConverters;

namespace CustomParser.Mapping
{
   public class PropertyMapping<TEntity, TProperty> : IPropertyMapping<TEntity, string>
      where TEntity : class, new()
   {
      private readonly ITypeConverter<TProperty> propertyConverter;
      private readonly string propertyName;
      private readonly Action<TEntity, TProperty> propertySetter;

      public PropertyMapping(Expression<Func<TEntity, TProperty>> property, ITypeConverter<TProperty> typeConverter)
      {
         propertyConverter = typeConverter;
         propertyName = ParserHelper.GetPropertyNameFromExpression(property);
         propertySetter = ParserHelper.CreateSetter(property);
      }

      public bool TryMapValue(TEntity entity, string value)
      {
         if (!propertyConverter.TryConvert(value, out TProperty convertedValue)) return false;

         propertySetter(entity, convertedValue);

         return true;
      }
   }
}