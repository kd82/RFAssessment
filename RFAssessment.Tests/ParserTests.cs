using CustomParser;
using CustomParser.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace RFAssessment.Tests
{
   [TestClass]
   public class ParserTests
   {
      private class Employee
      {
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public int Age { get; set; }
      }

      private class EmployeeMapping : Mapping<Employee>
      {
         public EmployeeMapping()
         {
            MapProperty(0, x => x.FirstName);
            MapProperty(1, x => x.LastName);
            MapProperty(2, x => x.Age);
         }
      }
      [Test]
      public void ReadFromFile_Null_Test()
      {
         Parser<Employee> csvParser = new Parser<Employee>(',', new EmployeeMapping());
         Assert.ThrowsException<ArgumentNullException>(() => csvParser.ReadFromFile(null));
      }

      [Test]
      public void ReadFromFileTest()
      {
         Parser<Employee> csvParser = new Parser<Employee>(',', new EmployeeMapping());
         StringBuilder stringBuilder = new StringBuilder()
            .AppendLine("FirstName;LastName;Age")
            .AppendLine("Krishna;D;22")
            .AppendLine("Max;Young;33");

         string basePath = AppDomain.CurrentDomain.BaseDirectory;
         string filePath = Path.Combine(basePath, "test_file.txt");

         File.WriteAllText(filePath, stringBuilder.ToString(), Encoding.UTF8);
         List<MappingResult<Employee>> result = csvParser
             .ReadFromFile(filePath)
             .ToList();

         Assert.AreEqual(2, result.Count);

         Assert.IsTrue(result.All(x => x.IsValid));

         Assert.AreEqual("Krishna", result[0].Result.FirstName);
         Assert.AreEqual("D", result[0].Result.LastName);

         Assert.AreEqual(22, result[0].Result.Age);
        

         Assert.AreEqual("Max", result[1].Result.FirstName);
         Assert.AreEqual("Young", result[1].Result.LastName);
         Assert.AreEqual(33, result[1].Result.Age);
        
      }
   }
}