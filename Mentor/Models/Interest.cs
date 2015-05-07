using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mentor.Models;

namespace Mentor.Models
{
    public class Interest
    {

        public Interest()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.ProgramInterests = new HashSet<Program>();
        }

        public int InterestId { get; set; }
        public string Name { get; set; }


        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Program> ProgramInterests { get; set; } 

    }
}
