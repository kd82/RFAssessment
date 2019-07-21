using CustomParser.Tokenizer;

namespace CustomParser
{
   public class ParserOptions
   {
      public readonly ITokenizer Tokenizer;

      public ParserOptions(char delimeter)
      {
         Tokenizer = new StringSplitTokenizer(new[] {delimeter});
      }
   }
}