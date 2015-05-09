using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;

namespace Mentor.ViewModels
{
    public class ProfileViewModel
    {
        public List<Program> Programs { get; set; }
        public List<User> Users { get; set; }
    }
}