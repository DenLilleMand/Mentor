using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models
{
    public class Mentor
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string MentorProfileText { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual  ICollection<Program> Programs { get; set; }
    }
}
