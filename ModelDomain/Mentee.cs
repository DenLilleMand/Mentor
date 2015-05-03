using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDomain
{
    public class Mentee
    {
        [Key,ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public string ProfileText { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
    }
}
