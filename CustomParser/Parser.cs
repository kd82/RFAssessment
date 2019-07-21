using System;
using System.Collections.Generic;
using System.Linq;
using CustomParser.Mapping;
using CustomParser.Models;

namespace CustomParser
{
   public class Parser<TEntity> : IParser<TEntity>
   {
      private readonly IMapping<TEntity> mapping;
      private readonly ParserOptions options;

      public Parser(ParserOptions options, IMapping<TEntity> mapping)
      {
         this.options = options;
         this.mapping = mapping;
      }

      public IEnumerable<MappingResult<TEntity>> Parse(IEnumerable<Row> data)
      {
         if (data == null) throw new ArgumentNullException(nameof(data));

         ParallelQuery<Row> query = data.AsParallel();

         query = query.Where(row => !string.IsNullOrWhiteSpace(row.Data));

         return query.Select(line => new TokenizedRow(line.Index, options.Tokenizer.Tokenize(line.Data)))
            .Select(fields => mapping.Map(fields));
      }
   }
}