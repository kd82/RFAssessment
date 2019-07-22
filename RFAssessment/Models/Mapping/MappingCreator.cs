using CustomParser.Mapping;
using RFAssessment.Models.MappingModels;
using System;

namespace RFAssessment.Models.Mapping
{
   public class MappingCreator:IMappingCreator
   {
      public  Mapping<CustomerModel> CreateMapping(char delimeter)
      {
         switch (delimeter)
         {
            case ',':
               return new CustomerCommaMapping();
            case '#':
               return new CustomerHashMapping();
            case '-':
               return new CustomerHyphenMapping();
            default:
               throw new NotSupportedException("Delimeter Not Supported");
         }
      }
   }
}