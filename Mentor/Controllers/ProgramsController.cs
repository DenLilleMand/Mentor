using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mentor.Controllers
{
    public class ProgramsController : Controller
    {
        // GET: Programs
        public ActionResult Index()
        {   
            return View();
        }
    }
}