namespace CustomParser.Models
{
   public class TokenizedRow
   {
      public readonly int Index;

      public readonly string[] Tokens;

      public TokenizedRow(int index, string[] tokens)
      {
         Index = index;
         Tokens = tokens;
      }
   }
}