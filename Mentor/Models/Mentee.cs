using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models
{
    public class Mentee
    {
        [Key]
        public int MenteeId { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
