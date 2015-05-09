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
        private UserRepository _userRepository = new UserRepository();
        private ProgramRepository _programRepository = new ProgramRepository();

        // GET: Utility
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSearchData(string input)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.Programs = _programRepository.Search(input).ToList();
            profileViewModel.Users = _userRepository.Search(input).ToList();
            var finishSearchList = JsonConvert.SerializeObject(profileViewModel, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Content(finishSearchList, "application/json");

        }

    }
}