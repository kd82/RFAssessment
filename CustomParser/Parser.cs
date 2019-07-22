using System;
using System.Collections.Generic;
using System.Linq;
using CustomParser.Mapping;
using CustomParser.Models;
using CustomParser.Tokenizer;

namespace CustomParser
{
   public class Parser<TEntity> : IParser<TEntity>
   {
      private readonly IMapping<TEntity> mapping;

      private readonly ITokenizer tokenizer;

      public Parser(char delimeter, IMapping<TEntity> mapping)
      {
         this.mapping = mapping;
         this.tokenizer = new StringSplitTokenizer(new []{delimeter});
      }

      public IEnumerable<MappingResult<TEntity>> Parse(IEnumerable<Row> data)
      {
         if (data == null) throw new ArgumentNullException(nameof(data));

         ParallelQuery<Row> query = data.AsParallel();

         query = query.Where(row => !string.IsNullOrWhiteSpace(row.Data));

         return query.Select(line => new TokenizedRow(line.Index, this.tokenizer.Tokenize(line.Data)))
            .Select(fields => mapping.Map(fields));
      }
   }
}