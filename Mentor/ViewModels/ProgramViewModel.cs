using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models;

namespace Mentor.ViewModels
{
    public class ProgramViewModel
    {

        public List<User> Users { get; set; }
        public User CurrentUser { get; set; }
        public List<ProgramMessage> ProgramMessages { get; set; } 
        public Program Program { get; set; }
        public Boolean IsAdmin { get; set; }
        public Boolean IsMentor { get; set; }
        public Boolean IsMentee { get; set; }
    }
}