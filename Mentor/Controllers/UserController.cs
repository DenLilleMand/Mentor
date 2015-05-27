using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Mentor.Models;
using Mentor.Models.Repositories.ConcreteImplementation;
using Mentor.Models.Repositories.Interfaces;
using Mentor.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace Mentor.Controllers
{


    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        // GET: Profile
        public ActionResult Index(int? id)
        {
            //This check should make it possible for our login screen to redirect,
            //to the current user index page, without having access to the UserId from the accountController.
            //Im not entirely sure why User.Identity.GetUserId() doesnt work in the Login Action, 
            //but this is the most reasonable fix i have for the issue.  Obviously we can still call this get,
            //without being logged in, and would get an error, because the id is null, and the User.Identity.getUserId()
            //would return an empty string. So we should find some Authenticated Annotation to put on this action?
            if (id == null)
            {
                return View(_userRepository.Read(Int32.Parse(User.Identity.GetUserId()))); ;
            }
            return View(_userRepository.Read(id));
        }

        //GET: Interests
        [HttpGet]
        public ActionResult Interests()
        {
                User currentUser = _userRepository.Read(Convert.ToInt32(User.Identity.GetUserId()));
                InterestViewModel interestViewModel = new InterestViewModel();
                interestViewModel.CurrentUser = currentUser;
                currentUser.LastLogin = new DateTime();
                _userRepository.Update(currentUser);
                return View(interestViewModel);
        }

        [HttpPost]
        public ActionResult Interests(InterestViewModel model)
        {
            Debug.WriteLine("hello from Interests post");
            Debug.WriteLine("interests:" + model.Interests);
            int userId  = Int32.Parse(User.Identity.GetUserId());
            User user =_userRepository.Read(userId);
            model.Interests.ForEach(delegate(string name)
            {
                user.UndefinedInterests.Add(new Interest()
                {
                    Name = name 
                });
            });
            _userRepository.Update(user);
            return Content(new JavaScriptSerializer().Serialize(new InterestViewModel()
            {
                RedirectUrl = "/User/index/" + userId
            }), "application/json");
        }

        //                [HttpPost]
        //                public ActionResult GetSearchData(string input)
        //                {
        //                    ProfileViewModel profileViewModel = new ProfileViewModel();
        //                    profileViewModel.Programs = _programRepository.Search(input).ToList();
        //                    profileViewModel.Users = _userRepository.Search(input).ToList();
        //                    //var finishSearchList = JsonConvert.SerializeObject(profileViewModel, Formatting.None, new JsonSerializerSettings()
        //                  //  {
        //                  //      ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //                  //  });
        //                    return Json(profileViewModel, "application/json");
        //             
        //            }
    }
}