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


        private UserRepository _userRepository = new UserRepository();
        private ProgramRepository _programRepository = new ProgramRepository();
        private ProfileViewModel profileViewModel = new ProfileViewModel();

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
