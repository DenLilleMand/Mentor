using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Mentor.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Mentor.Controllers
{


    public class UserController : Controller
    {
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index(int id)
        {
            
          Image test  =Image.FromFile( Server.MapPath("~/pictures/b.png"));
           
            User user = db.Users.Find(id);
            user.Picture = imageToByteArray(test);
            Image i = byteArrayToImage(user.Picture);
           

            return View(user);
        }

        [HttpPost]
        public ActionResult GetSearchData(string input)
        {
            IEnumerable<Program> searchMentorPrograms = db.Set<Program>().Where((n => n.Name.Contains(input)));
            IEnumerable<User>searchResult = db.Users.Where(n => n.FirstName.Contains(input) || n.LastName.Contains(input));
           
            List<Object> test = new List<object>();
            test.AddRange(searchResult);
                 test.AddRange(searchMentorPrograms);
                 var personList = JsonConvert.SerializeObject(test, Formatting.None, new JsonSerializerSettings()
                 {
         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    });

                 return Content(personList, "application/json");


        }
    }
}