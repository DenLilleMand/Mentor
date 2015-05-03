using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public class Mentor
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string MentorProfileText { get; set; }
        
        public virtual  ICollection<MentorProgram> MentorPrograms { get; set; }
    }
}
