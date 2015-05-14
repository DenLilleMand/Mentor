using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Mentor.Models.Repositories.Concrete_Implementation
{
    public class UtilityRepository
    {

        //Hardcoded dependencies(Unable to mock it), wrong naming conventions(instance variables starts with capital letter). Doesn't implement the IRepository Interface - 
        //which doesn't exactly make sense for it either, because the only method in this repository we need is search, Makes me wonder, if the UtilityRepository, is actually 
        //unnessecery, and we should give whatever code that calls this, the access to these repositores it self. food for thought.
        private UserRepository _userRepository = new UserRepository();
        private ProgramRepository _programRepository = new ProgramRepository();
        

        public string Search(string input)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.Programs = _programRepository.Search(input).ToList();
            profileViewModel.Users = _userRepository.Search(input).ToList();
            var finishSearchList = JsonConvert.SerializeObject(profileViewModel, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
            return finishSearchList;
        }
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
    }
}
