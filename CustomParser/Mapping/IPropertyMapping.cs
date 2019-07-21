namespace CustomParser.Mapping
{
   public interface IPropertyMapping<in TEntity, in TProperty>
   {
      bool TryMapValue(TEntity entity, TProperty value);
   }
}