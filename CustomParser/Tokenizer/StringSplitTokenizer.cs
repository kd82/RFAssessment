namespace CustomParser.Tokenizer
{
   internal class StringSplitTokenizer : ITokenizer
   {
      public readonly char[] FieldsSeparator;

      public StringSplitTokenizer(char[] fieldsSeparator)
      {
         FieldsSeparator = fieldsSeparator;
      }

      public string[] Tokenize(string input)
      {
         return input.Split(FieldsSeparator);
      }
   }
}