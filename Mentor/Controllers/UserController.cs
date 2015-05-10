using System.Web.Mvc;
using Mentor.Models;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Controllers
{


    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        // GET: Profile
        public ActionResult Index(int id)
        {

            return View(_userRepository.Read(id));

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