using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models
{
    public class Mentor
    {
        [Key]
        public int MentorId { get; set; }
        public int Rating { get; set; }
        public virtual  ICollection<Program> Programs { get; set; }
    }
}
