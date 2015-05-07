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
            this.MentorUsers = new HashSet<User>();
            this.MenteeUsers = new HashSet<User>();
            this.UndefinedUsers = new HashSet<User>();

            this.ProgramInterests = new HashSet<Program>();
        }

        public int InterestId { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Program> ProgramInterests { get; set; }
        public virtual ICollection<User> MentorUsers { get; set; }
        public virtual ICollection<User> MenteeUsers { get; set; }
        public virtual ICollection<User> UndefinedUsers { get; set; } 

    }
}
