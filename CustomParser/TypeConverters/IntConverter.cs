using System;
using System.Globalization;

namespace CustomParser.TypeConverters
{
   public class IntConverter : NonNullableConverter<int>
   {
      private readonly IFormatProvider formatProvider;
      private readonly NumberStyles numberStyles;

      public IntConverter()
         : this(CultureInfo.InvariantCulture)
      {
      }

      public IntConverter(IFormatProvider formatProvider, NumberStyles numberStyles = NumberStyles.Integer)
      {
         this.formatProvider = formatProvider;
         this.numberStyles = numberStyles;
      }

      protected override bool InternalConvert(string value, out int result)
      {
         return int.TryParse(value, numberStyles, formatProvider, out result);
      }
   }
}