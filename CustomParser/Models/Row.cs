namespace CustomParser.Models
{
   public class Row
   {
      public readonly string Data;
      public readonly int Index;

      public Row(int index, string data)
      {
         Index = index;
         Data = data;
      }
   }
}