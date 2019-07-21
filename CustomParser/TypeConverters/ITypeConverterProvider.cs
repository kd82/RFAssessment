namespace CustomParser.TypeConverters
{
   public interface ITypeConverterProvider
   {
      ITypeConverter<TTargetType> Resolve<TTargetType>();
   }
}