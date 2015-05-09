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
        public Program Program { get; set; }
    }
}