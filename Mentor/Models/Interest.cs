using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mentor.Models;

namespace Mentor.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
