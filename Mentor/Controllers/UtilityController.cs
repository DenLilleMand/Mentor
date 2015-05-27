using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Mentor.Models.Repositories.Interfaces;
using Mentor.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Mentor.Controllers
{
    public class UtilityController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly ProgramRepository _programRepository;

        public UtilityController( UserRepository userRepository, ProgramRepository programRepository)
        {
            _userRepository = userRepository;
            _programRepository = programRepository;
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

        [HttpGet]
        public ActionResult GetUserFirstName()
        {
            return Content(_userRepository.Read(Convert.ToInt32(User.Identity.GetUserId())).FirstName);
        }
       

    }
}