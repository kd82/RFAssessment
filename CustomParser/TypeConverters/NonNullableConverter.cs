namespace CustomParser.TypeConverters
{
   public abstract class NonNullableConverter<TTargetType> : BaseConverter<TTargetType>
   {
      public override bool TryConvert(string value, out TTargetType result)
      {
         if (!string.IsNullOrWhiteSpace(value)) return InternalConvert(value, out result);
         result = default(TTargetType);

         return false;
      }

      protected abstract bool InternalConvert(string value, out TTargetType result);
   }
}