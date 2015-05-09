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
using Mentor.Models.Repositories.Interfaces;
using Mentor.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Mentor.Controllers
{


    public class UserController : Controller
    {
        private IRepository<User> _userRepository;

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

        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        // GET: Profile
        public ActionResult Index(int? id)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.User = _userRepository.Read(id);
            return View(profileViewModel);
        }

//        [HttpPost]
//        public ActionResult GetSearchData(string input)
//        {
//            ProfileViewModel profileViewModel = new ProfileViewModel();
//            profileViewModel.Programs = _programRepository.Search(input).ToList();
//            profileViewModel.Users = _userRepository.Search(input).ToList();
//            var finishSearchList = JsonConvert.SerializeObject(profileViewModel, Formatting.None, new JsonSerializerSettings()
//            {
//                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//            });
//            return Content(finishSearchList, "application/json");
//     
//    }
    }
}