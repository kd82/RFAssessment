using System;

namespace CustomParser.TypeConverters
{
   public interface ITypeConverter
   {
   }

   public interface ITypeConverter<TTargetType> : ITypeConverter
   {
      Type TargetType { get; }
      bool TryConvert(string value, out TTargetType result);
   }
}