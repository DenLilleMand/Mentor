using System.Collections.Generic;
using System.Data.Entity.Validation;
using Mentor.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            try
            {
                var passwordHasher = new PasswordHasher();
                User user1 = new User()
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
                    UndefinedInterests = new List<Interest>
                    {

                    },
                    MentorInterests = new List<Interest>
                    {

                    },
                    MenteeInterests = new List<Interest>
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

                    },
                    CreatorForPrograms = new List<Program>()
                    {

                    },
                    ProgramMessages = new List<ProgramMessage>()
                    {

                    }
                };
                User user2 = new User()
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
                    UndefinedInterests = new List<Interest>
                    {

                    },
                    MenteeInterests = new List<Interest>
                    {

                    },
                    MentorInterests = new List<Interest>
                    {

                    },
                    MentorPrograms = new List<Program>
                    {

                    },
                    MenteePrograms = new List<Program>
                    {

                    },
                    AdminForPrograms = new List<Program>
                    {

                    },
                    CreatorForPrograms = new List<Program>()
                    {

                    },
                    ProgramMessages = new List<ProgramMessage>()
                    {

                    }
                };

                Interest interest1 = new Interest
                {
                    Name = "Meteor",
                    UndefinedUsers = new List<User>
                    {

                    },
                    MentorUsers = new List<User>
                    {
                        user1
                    },
                    MenteeUsers = new List<User>
                    {
                        user2
                    }
                };


                Interest interest2 = new Interest()
                {
                    Name = "asp.net mvc",
                    UndefinedUsers = new List<User>
                    {
                        user1
                    },
                    MentorUsers = new List<User>
                    {
                        user2
                    },
                    MenteeUsers = new List<User>
                    {

                    }
                };


                Interest interest3 = new Interest()
                {
                    Name = "php",
                    UndefinedUsers = new List<User>
                    {
                        user2
                    },
                    MentorUsers = new List<User>
                    {

                    },
                    MenteeUsers = new List<User>
                    {
                        user1
                    }
                };
                //not entirely sure if these are nessecery.
                user1.MentorInterests.Add(interest1);

                user1.UndefinedInterests.Add(interest2);

                user1.MenteeInterests.Add(interest3);

                user2.MenteeInterests.Add(interest1);

                user2.MentorInterests.Add(interest2);

                user2.UndefinedInterests.Add(interest3);



                Program program1 = new Program()
                {
                    Name = "My Meteor program",
                    Mentors = new List<User>()
                    {
                        user1
                    },
                    Mentee = new List<User>()
                    {
                        user2
                    },
                    Admins = new List<User>()
                    {
                        user1
                    },
                    Creator = user1,
                    Visibility = Visibility.Private,
                    Interest = interest1,
                    ProgramMessages = new List<ProgramMessage>()
                    {

                    },
                    Picture = new byte[1024]

                };

                ProgramMessage message1 = new ProgramMessage()
                {
                    Message = "Jeg ved det godt. ",
                    User = user1,
                    Program = program1,
                    Date = new DateTime()
                };

                ProgramMessage message2 = new ProgramMessage()
                {
                    Message = "Matti du er en gud. Nanananananananannanann batmaN! nananananannnnnananaa",
                    User = user2,
                    Program = program1,
                    Date = new DateTime()
                };
                user1.ProgramMessages.Add(message1);
                user2.ProgramMessages.Add(message2);

                program1.ProgramMessages.Add(message1);
                program1.ProgramMessages.Add(message2);

                user1.MentorPrograms.Add(program1);
                user1.CreatorForPrograms.Add(program1);
                user2.MenteePrograms.Add(program1);

                if (!context.Users.Any(u => u.UserName == "mattinielsen5@hotmail.com"))
                {
                    var store = new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(context);
                    var manager = new UserManager<User, int>(store);
                    manager.Create(user2, "Denlilleiceman20!");
                    manager.Create(user1, "Denlilleiceman20!");
                }

                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}