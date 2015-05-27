using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models 
{

    public enum Visibility
    {
        Private,
        Public
    }

    public class Program : IContextEntity
    {
        #region public
        public Program()
        {
            Mentors = new HashSet<User>();
            Mentee = new HashSet<User>();
            Admins = new HashSet<User>();
            ProgramMessages = new HashSet<ProgramMessage>();
            ProgramApplications = new HashSet<ProgramApplication>();
            Notifications = new HashSet<Notification>();
        }

        public Program(Interest interest, User creator, string description)
        {
            Interest = interest;
            Creator = creator;
            Description = description;
            Mentors = new HashSet<User>();
            Mentee = new HashSet<User>();
            Admins = new HashSet<User>();
            ProgramMessages = new HashSet<ProgramMessage>();
            ProgramApplications = new HashSet<ProgramApplication>();
            Notifications = new HashSet<Notification>();
        }


        public int Id { get; set; }
        public Visibility Visibility { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }


       
        public int CreatorId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InterestId { get; set; }


        public virtual ICollection<User> Admins { get; set; }
        public virtual ICollection<User> Mentors { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        
        public virtual ICollection<User> Mentee { get; set; }//shouldve been mentees obviously :/  if any1 has time to fix.
        public virtual ICollection<ProgramMessage> ProgramMessages { get; set; }
        public virtual ICollection<ProgramApplication> ProgramApplications { get; set; }


        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        [ForeignKey("InterestId")]
        public virtual Interest Interest { get; set; }
        #endregion
    }
    
}
