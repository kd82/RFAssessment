using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CustomParser.Models;
using CustomParser.TypeConverters;

namespace CustomParser.Mapping
{
   public abstract class Mapping<TEntity> : IMapping<TEntity>
      where TEntity : class, new()
   {
      private readonly List<IndexToPropertyMapping> csvIndexPropertyMappings;

      private readonly ITypeConverterProvider typeConverterProvider;

      protected Mapping()
         : this(new TypeConverterProvider())
      {
      }

      private Mapping(ITypeConverterProvider typeConverterProvider)
      {
         this.typeConverterProvider = typeConverterProvider;
         csvIndexPropertyMappings = new List<IndexToPropertyMapping>();
      }

      public MappingResult<TEntity> Map(TokenizedRow values)
      {
         TEntity entity = new TEntity();

         // Iterate over Index Mappings:
         foreach (IndexToPropertyMapping indexToPropertyMapping in csvIndexPropertyMappings)
         {
            int columnIndex = indexToPropertyMapping.ColumnIndex;

            if (columnIndex >= values.Tokens.Length)
               return new MappingResult<TEntity>
               {
                  RowIndex = values.Index,
                  Error = new MappingError
                  {
                     ColumnIndex = columnIndex,
                     Value = $"Column {columnIndex} is Out Of Range",
                     UnmappedRow = string.Join("|", values.Tokens)
                  }
               };

            string value = values.Tokens[columnIndex];

            if (!indexToPropertyMapping.PropertyMapping.TryMapValue(entity, value))
               return new MappingResult<TEntity>
               {
                  RowIndex = values.Index,
                  Error = new MappingError
                  {
                     ColumnIndex = columnIndex,
                     Value = $"Column {columnIndex} with Value '{value}' cannot be converted",
                     UnmappedRow = string.Join("|", values.Tokens)
                  }
               };
         }


         return new MappingResult<TEntity>
         {
            RowIndex = values.Index,
            Result = entity
         };
      }

      protected PropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Expression<Func<TEntity, TProperty>> property)
      {
         return MapProperty(columnIndex, property, typeConverterProvider.Resolve<TProperty>());
      }

      private PropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Expression<Func<TEntity, TProperty>> property, ITypeConverter<TProperty> typeConverter)
      {
         if (csvIndexPropertyMappings.Any(x => x.ColumnIndex == columnIndex)) throw new InvalidOperationException($"Duplicate mapping for column index {columnIndex}");

         PropertyMapping<TEntity, TProperty> propertyMapping = new PropertyMapping<TEntity, TProperty>(property, typeConverter);

         AddPropertyMapping(columnIndex, propertyMapping);

         return propertyMapping;
      }

      private void AddPropertyMapping<TProperty>(int columnIndex, PropertyMapping<TEntity, TProperty> propertyMapping)
      {
         IndexToPropertyMapping indexToPropertyMapping = new IndexToPropertyMapping
         {
            ColumnIndex = columnIndex,
            PropertyMapping = propertyMapping
         };

         csvIndexPropertyMappings.Add(indexToPropertyMapping);
      }

      private class IndexToPropertyMapping
      {
         public int ColumnIndex { get; set; }

         public IPropertyMapping<TEntity, string> PropertyMapping { get; set; }
      }
   }
}