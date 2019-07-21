using System;
using System.Collections.Generic;

namespace CustomParser.TypeConverters
{
   public class TypeConverterProvider : ITypeConverterProvider
   {
      private readonly IDictionary<Type, ITypeConverter> typeConverters;

      public TypeConverterProvider()
      {
         typeConverters = new Dictionary<Type, ITypeConverter>();

         Add(new StringConverter());
         Add(new IntConverter());
      }

      public ITypeConverter<TTargetType> Resolve<TTargetType>()
      {
         Type targetType = typeof(TTargetType);

         if (!typeConverters.TryGetValue(targetType, out ITypeConverter typeConverter)) throw new NotSupportedException("No such typeConverter has been registered");

         return typeConverter as ITypeConverter<TTargetType>;
      }

      public TypeConverterProvider Add<TTargetType>(ITypeConverter<TTargetType> typeConverter)
      {
         typeConverters[typeConverter.TargetType] = typeConverter;
         return this;
      }
   }
}