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
            this.ProgramMessages = new HashSet<ProgramMessage>();
        }

        public int Id { get; set; }
        public Visibility Visibility { get; set; }
        public string Name { get; set; }

       
        public int CreatorId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestId { get; set; }


        public virtual ICollection<User> Admins { get; set; }
        public virtual ICollection<User> Mentors { get; set; }
<<<<<<< HEAD
        public virtual ICollection<User> Mentee { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        
=======
        public virtual ICollection<User> Mentee { get; set; }//shouldve been mentees obviously :/  if any1 has time to fix.
        public virtual ICollection<ProgramMessage> ProgramMessages { get; set; }


>>>>>>> 474622ea6863c3161e7519f8ee1f5914ff4f1797
        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        [ForeignKey("InterestId")]
        public virtual Interest Interest { get; set; }
    }
    #endregion
}
