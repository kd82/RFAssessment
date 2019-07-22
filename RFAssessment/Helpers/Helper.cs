using System;
using System.Collections.Generic;
using System.Linq;
using CustomParser.Mapping;
using RFAssessment.Models;

namespace RFAssessment.Helpers
{
   public static class Helper
   {
      public static List<MappingResult<CustomerModel>> ProcessEachMapping(IEnumerable<MappingResult<CustomerModel>> cus,char delimeter)
      {
         switch (delimeter)
         {
            case '#':
               return cus.Select(x =>
               {
                  x.Result.YearsInBusiness = DateTime.Now.Year - x.Result.YearsInBusiness; return x;
               }).ToList();
            case '-':
               return cus.Select(x =>
               {
                  x.Result.YearsInBusiness = DateTime.Now.Year - x.Result.YearsInBusiness;
                  x.Result.ContactFirstName = x.Result.ContactFirstName + " " + x.Result.ContactLastName;
                  return x;
               }).ToList();
            default:
               return cus.ToList();

         }
      }
   }
}