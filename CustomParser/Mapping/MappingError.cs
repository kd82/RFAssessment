namespace CustomParser.Mapping
{
   public class MappingError
   {
      public int ColumnIndex { get; set; }

      public string Value { get; set; }

      public string UnmappedRow { get; set; }
   }
}