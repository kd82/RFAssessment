namespace CustomParser.Mapping
{
   public class MappingResult<TEntity>
   {
      public int RowIndex { get; set; }

      public MappingError Error { get; set; }

      public TEntity Result { get; set; }

      public bool IsValid
      {
         get => Error == null;
      }
   }
}