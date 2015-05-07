using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mentor.Models
{
    #region testingRegions
    public enum Visibility
    {
        Private,
        Public
    }

    public class Program
    {
        
        public Program()
        {
            this.Mentors = new HashSet<ApplicationUser>();
            this.Mentee = new HashSet<ApplicationUser>();
            this.Admins = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public Visibility Visibility { get; set; }
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestId { get; set; }


        [ForeignKey("InterestId")]
        public virtual Interest Interest { get; set; }
        public virtual ICollection<ApplicationUser> Admins { get; set; }
        public virtual ICollection<ApplicationUser> Mentors { get; set; }
        public virtual ICollection<ApplicationUser> Mentee  { get; set; }
        


    }
    #endregion
}
