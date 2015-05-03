using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDomain
{
    public class Interest
    {
        [Key, ForeignKey("ApplicationUser")]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}
