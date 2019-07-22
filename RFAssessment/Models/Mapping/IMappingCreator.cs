using CustomParser.Mapping;

namespace RFAssessment.Models.Mapping
{
   public interface IMappingCreator
   {
      Mapping<CustomerModel> CreateMapping(char delimeter);
   }
}