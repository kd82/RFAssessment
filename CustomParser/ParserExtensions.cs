using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CustomParser.Mapping;
using CustomParser.Models;

namespace CustomParser
{
   public static class ParserExtensions
   {
      public static IEnumerable<MappingResult<TEntity>> ReadFromFile<TEntity>(this Parser<TEntity> csvParser, string fileName)
      {
         if (fileName == null) throw new ArgumentNullException(nameof(fileName));

         IEnumerable<Row> lines = File.ReadLines(fileName, Encoding.ASCII).Select((line, index) => new Row(index, line));

         return csvParser.Parse(lines);
      }
   }
}