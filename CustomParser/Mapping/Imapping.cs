using CustomParser.Models;

namespace CustomParser.Mapping
{
   public interface IMapping<TEntity>
   {
      MappingResult<TEntity> Map(TokenizedRow values);
   }
}