using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Mentor.Models;
using Mentor.Models.Repositories;
using Mentor.Models.Repositories.Concrete_Implementation;
using Mentor.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Mentor.Controllers
{
    

    public class UserController : Controller
    {
        private UserRepository userRepository = new UserRepository();
        private ProgramRepository programRepository = new ProgramRepository();
       // public byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
         //   MemoryStream ms = new MemoryStream();
          //  imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
          //  return ms.ToArray();
       // }
       // public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        //{
         //   MemoryStream ms = new MemoryStream(byteArrayIn);
          //  System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
           // return returnImage;
       // }

      

        // GET: Profile
        public ActionResult Index(int id)
        {
            
          //Image test  =Image.FromFile( Server.MapPath("~/pictures/b.png"));
           
           // User user = db.Users.Find(id);
           // user.Picture = imageToByteArray(test);
           // Image i = byteArrayToImage(user.Picture);
           

            return View(userRepository.Read(id));
        }

        [HttpPost]
        public ActionResult GetSearchData(string input)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.Programs = programRepository.Search(input).ToList();
            profileViewModel.Users = userRepository.Search(input).ToList();
            var finishSearchList = JsonConvert.SerializeObject(profileViewModel, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return Content(finishSearchList, "application/json");

        /*    IEnumerable<Program> searchMentorPrograms = programRepository.Search(input);
            IEnumerable<User> searchUsers = userRepository.Search(input);
           // Skal fjernes væk herfra!
            //List<Object> combinedSearchResults = new List<object>();
           // combinedSearchResults.AddRange(searchUsers);
            //     combinedSearchResults.AddRange(searchMentorPrograms);
                 var personList = JsonConvert.SerializeObject(searchMentorPrograms, Formatting.None, new JsonSerializerSettings()
                 {
         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });
                 var personList2 = JsonConvert.SerializeObject(searchUsers, Formatting.None, new JsonSerializerSettings()
                 {
                     ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                 });

                 return Content(personList, "application/json");
*/

        }

        private ActionResult Content(string personList, string personList2, string p)
        {
            throw new NotImplementedException();
        }
    }
}