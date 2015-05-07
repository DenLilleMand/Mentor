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
            this.Mentors = new HashSet<User>();
            this.Mentee = new HashSet<User>();
            this.Admins = new HashSet<User>();
        }

        public int Id { get; set; }
        public Visibility Visibility { get; set; }
        public string Name { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestId { get; set; }


        [ForeignKey("InterestId")]
        public virtual Interest Interest { get; set; }
        public virtual ICollection<User> Admins { get; set; }
        public virtual ICollection<User> Mentors { get; set; }
        public virtual ICollection<User> Mentee { get; set; }
        


    }
    #endregion
}
