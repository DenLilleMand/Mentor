using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models
{
    public enum Visibility
    {
        Private,
        Public
    }

    public class Program
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }

        [ForeignKey("Interest")]
        public int InterestId { get; set; }


        public Visibility Visibility { get; set; }
        public string Name { get; set; } 
        public int Rating { get; set; }
        public byte[] Picture { get; set; }

        public virtual Interest Interest { get; set; }
        public virtual ICollection<Mentor> Mentors { get; set; }
        public virtual ICollection<Mentee> Mentees { get; set; }
    }
}
