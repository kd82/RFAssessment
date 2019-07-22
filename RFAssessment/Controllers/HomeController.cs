using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CustomParser;
using CustomParser.Mapping;
using RFAssessment.Helpers;
using RFAssessment.Models;
using RFAssessment.Models.Mapping;
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

      private readonly IMappingCreator _mapping;

      public HomeController(IMappingCreator mapping)
      {
         _mapping = mapping;
      }

      public ActionResult LoadData()
      {
         List<MappingResult<CustomerModel>> customers = new List<MappingResult<CustomerModel>>();
         Dictionary<char, string> dictionary = new Dictionary<char, string> {{'#', Server.MapPath("~/Uploads/")+"hash.txt" },
                                                                             { ',', Server.MapPath("~/Uploads/")+"comma.txt" },
                                                                             {'-',Server.MapPath("~/Uploads/")+"hyphen.txt"}};
         foreach (KeyValuePair<char, string> kvPair in dictionary)
         {
            Parser<CustomerModel> parser = new Parser<CustomerModel>(kvPair.Key, _mapping.CreateMapping(kvPair.Key));
            customers.AddRange(Helper.ProcessEachMapping(parser.ReadFromFile(kvPair.Value),kvPair.Key));
         }
        
         return Json(new {data = customers}, JsonRequestBehavior.AllowGet);
      }
   }
}