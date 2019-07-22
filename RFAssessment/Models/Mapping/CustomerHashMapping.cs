using CustomParser.Mapping;

namespace RFAssessment.Models.MappingModels
{
   public class CustomerHashMapping : Mapping<CustomerModel>
   {
      public CustomerHashMapping()
      {
         MapProperty(0, x => x.CompanyName);
         MapProperty(1, x => x.YearsInBusiness);
         MapProperty(2, x => x.ContactFirstName);
         MapProperty(3, x => x.ContactPhone);
      }
   }
}