using System;

namespace CustomParser.TypeConverters
{
   public abstract class BaseConverter<TTargetType> : ITypeConverter<TTargetType>
   {
      public abstract bool TryConvert(string value, out TTargetType result);

      public Type TargetType
      {
         get => typeof(TTargetType);
      }
   }
}