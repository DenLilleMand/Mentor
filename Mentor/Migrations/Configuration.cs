using System.Collections.Generic;
using Mentor.Models;
using Microsoft.AspNet.Identity;

namespace Mentor.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mentor.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            /*if (System.Diagnostics.Debugger.IsAttached == false)
               System.Diagnostics.Debugger.Launch();*/
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Mentor.Models.ApplicationDbContext";
        }

        protected override void Seed(Mentor.Models.ApplicationDbContext context)
        {
            var passwordHasher = new PasswordHasher();
            ApplicationUser user1 = new ApplicationUser()
            {
                UserName = "mattinielsen5@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Denlilleiceman20!"),
                FirstName = "Matti andreas",
                LastName = "Nielsen",
                Age = 24,
                ProfileText = "Lorem ipsum dolor sit amet, minimum delicatissimi ad eos, " +
                              "ne veniam eirmod voluptatibus vel, ne eam facilisi inciderint. " +
                              "Ex eleifend recteque delicatissimi eos, ut erat posse etiam pri." +
                              " Ei qui commune vivendum legendos, augue accusata in vim, mei at" +
                              " bonorum pericula definitionem. Has ornatus aliquando vulputate " +
                              "at, nonumes docendi in mel. Ne duo recusabo percipitur, et nam " +
                              "vitae nostrud cotidieque, cibo liber mel te.",
                IsMentor = true,
                IsMentee = false,
                Interests = new List<Interest>
                {
                    
                },
                MentorPrograms = new List<Program>()
                {

                },
                MenteePrograms = new List<Program>()
                {

                },
                AdminForPrograms = new List<Program>()
                {
                    
                }
            };
            ApplicationUser user2 = new ApplicationUser()
            {
                UserName = "martinbergpetersen@hotmail.com",
                PasswordHash = passwordHasher.HashPassword("Denlilleiceman20!"),
                FirstName = "Martin Berg",
                LastName = "Petersen",
                Age = 26,
                ProfileText = "Lorem ipsum dolor sit amet, minimum delicatissimi ad eos, " +
                              "ne veniam eirmod voluptatibus vel, ne eam facilisi inciderint. " +
                              "Ex eleifend recteque delicatissimi eos, ut erat posse etiam pri." +
                              " Ei qui commune vivendum legendos, augue accusata in vim, mei at" +
                              " bonorum pericula definitionem. Has ornatus aliquando vulputate " +
                              "at, nonumes docendi in mel. Ne duo recusabo percipitur, et nam " +
                              "vitae nostrud cotidieque, cibo liber mel te.",
                IsMentor = false,
                IsMentee = true,
                Interests = new List<Interest>
                {

                },
                MentorPrograms = new List<Program>()
                {

                },
                MenteePrograms = new List<Program>()
                {

                },
                AdminForPrograms = new List<Program>()
                {

                }
            };

            Interest interest1 = new Interest()
            {
                Name = "Meteor",
                ApplicationUsers = new List<ApplicationUser>()
                {
                    user1,user2
                }
            };
            Interest interest2 = new Interest()
            {
                Name = "asp.net mvc",
                ApplicationUsers = new List<ApplicationUser>()
                {
                    user1,user2
                }
            };
            Interest interest3 = new Interest()
            {
                Name = "php",
                ApplicationUsers = new List<ApplicationUser>()
                {
                    user1,user2
                }
            };
            
            //not entirely sure if these are nessecery.
            user1.Interests.Add(interest1);
            user1.Interests.Add(interest2);
            user1.Interests.Add(interest3);
            user2.Interests.Add(interest1);
            user2.Interests.Add(interest2);
            user2.Interests.Add(interest3);

            Program program1 = new Program()
            {
                Name = "My Meteor program",
                Mentors = new List<ApplicationUser>()
                {
                    user1
                },
                Mentee = new List<ApplicationUser>()
                {
                    user2
                },
                Admins = new List<ApplicationUser>()
                {
                    user1
                },
                Visibility = Visibility.Private
            };

            user1.MentorPrograms.Add(program1);
            user2.MenteePrograms.Add(program1);
            context.Users.AddOrUpdate(u => u.UserName, user1);
            context.Users.AddOrUpdate(u => u.UserName, user2);
            context.SaveChanges();
        }
    }
}
