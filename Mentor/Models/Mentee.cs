using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models.DomainModel
{
    public class Mentee
    {
        [Key,ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public string ProfileText { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
