using CustomParser.Mapping;

namespace RFAssessment.Models.MappingModels
{
   public class CustomerHyphenMapping : Mapping<CustomerModel>
   {
      public CustomerHyphenMapping()
      {
         MapProperty(0, x => x.CompanyName);
         MapProperty(1, x => x.YearsInBusiness);
         MapProperty(2, x => x.ContactPhone);
         MapProperty(3, x => x.ContactEmail);
         MapProperty(4, x => x.ContactFirstName);
         MapProperty(5, x => x.ContactLastName);
      }
   }
}