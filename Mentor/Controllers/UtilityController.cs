using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mentor.Models.Repositories.Concrete_Implementation;
using Mentor.ViewModels;
using Newtonsoft.Json;

namespace Mentor.Controllers
{
    public class UtilityController : Controller
    {
       private UtilityRepository _utilityRepository = new UtilityRepository();


        // GET: Utility
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSearchData(string input)
        {

            return Content(_utilityRepository.Search(input), "application/json");

        }
       

    }
}