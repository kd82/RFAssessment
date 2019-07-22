using CustomParser.Mapping;

namespace RFAssessment.Models.Mapping
{
   public class CustomerCommaMapping : Mapping<CustomerModel>
   {
      public CustomerCommaMapping()
      {
         MapProperty(0, x => x.CompanyName);
         MapProperty(1, x => x.ContactFirstName);
         MapProperty(2, x => x.ContactPhone);
         MapProperty(3, x => x.YearsInBusiness);
         MapProperty(4, x => x.ContactEmail);
      }
   }
}