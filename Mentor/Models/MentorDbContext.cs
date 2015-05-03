using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ModelDomain;

namespace Mentor.Models
{
    public class MentorDbContext : DbContext
    {
        public MentorDbContext() : base("defaultConnection")
        {
            
        }

        DbSet<Interest> InterestDbSet { get; set; }
       
        


    }
}