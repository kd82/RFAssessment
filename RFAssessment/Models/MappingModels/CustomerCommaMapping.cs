using CustomParser.Mapping;

namespace RFAssessment.Models.MappingModels
{
   public class CustomerCommaMapping : Mapping<CustomerModel>
   {
      public CustomerCommaMapping()
      {
         MapProperty(0, x => x.CompanyName);
         MapProperty(1, x => x.ContactName);
         MapProperty(2, x => x.ContactPhone);
         MapProperty(3, x => x.YearsInBusiness);
         MapProperty(4, x => x.ContactEmail);
      }
   }
}