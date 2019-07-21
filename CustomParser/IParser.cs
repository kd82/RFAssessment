using System.Collections.Generic;
using CustomParser.Mapping;
using CustomParser.Models;

namespace CustomParser
{
   public interface IParser<IEntity>
   {
      IEnumerable<MappingResult<IEntity>> Parse(IEnumerable<Row> data);
   }
}