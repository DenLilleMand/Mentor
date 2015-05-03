using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public enum Visibility
    {
        Private,
        Public
    }

    public class MentorProgram
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public Visibility Visibility { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public byte[] Picture { get; set; }

        public virtual ICollection<Mentor> Mentors { get; set; }
        public virtual ICollection<Mentee> Mentees { get; set; }
    }
}
