using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CustomParser;
using CustomParser.Mapping;
using RFAssessment.Models;
using RFAssessment.Models.MappingModels;

namespace RFAssessment.Controllers
{
   public class HomeController : Controller
   {
      // GET: Home
      public ActionResult Index()
      {
         return View();
      }

      public ActionResult LoadData()
      {
         List<MappingResult<CustomerModel>> customers = new List<MappingResult<CustomerModel>>();

         string serverPath = Server.MapPath("~/Uploads/");

         string commafilePath = serverPath + Path.GetFileName("comma.txt");
         ParserOptions options = new ParserOptions(',');
         CustomerCommaMapping mapping = new CustomerCommaMapping();
         Parser<CustomerModel> parser = new Parser<CustomerModel>(options, mapping);
         customers.AddRange(parser.ReadFromFile(commafilePath, Encoding.ASCII).ToList());

         string hashfilePath = Server.MapPath("~/Uploads/") + Path.GetFileName("hash.txt");
         options = new ParserOptions('#');
         CustomerHashMapping hashMapping = new CustomerHashMapping();
         parser = new Parser<CustomerModel>(options, hashMapping);
         List<MappingResult<CustomerModel>> custs = parser.ReadFromFile(hashfilePath, Encoding.ASCII).Select(x =>
         {
            x.Result.YearsInBusiness = DateTime.Now.Year - x.Result.YearsInBusiness;
            return x;
         }).ToList();

         customers.AddRange(custs);

         string hyphenfilePath = Server.MapPath("~/Uploads/") + Path.GetFileName("hyphen.txt");
         options = new ParserOptions('-');
         CustomerHyphenMapping hyphenMapping = new CustomerHyphenMapping();
         parser = new Parser<CustomerModel>(options, hyphenMapping);
         custs = parser.ReadFromFile(hyphenfilePath, Encoding.ASCII).Select(x =>
         {
            x.Result.YearsInBusiness = DateTime.Now.Year - x.Result.YearsInBusiness;
            x.Result.ContactName = x.Result.ContactFirstName + " " + x.Result.ContactLastName;
            return x;
         }).ToList();

         customers.AddRange(custs);
         return Json(new {data = customers}, JsonRequestBehavior.AllowGet);
      }
   }
}