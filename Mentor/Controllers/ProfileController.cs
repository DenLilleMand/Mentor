using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Microsoft.Ajax.Utilities;

namespace Mentor.Controllers
{
  
     
    public class ProfileController : Controller
    {
        List<string> test = new List<string>();

      
        // GET: Profile
        public ActionResult Index()
        {
          return View();
        }
 [HttpPost]
        public  ActionResult  GetSearchData(string input)
 {

             test.Add("Matti");
            test.Add("Hans");
            return Json(test, JsonRequestBehavior.AllowGet);
        }

    }
}