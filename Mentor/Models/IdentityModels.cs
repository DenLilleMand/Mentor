using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mentor.Models.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mentor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IContextEntity
    {
        public User()
        {
        }

        /*
         *  IdentityUser allready has an attribut called 'Id', so we dont have to make one,
         *  Thats why we're still able to create foreignkey relationships to other entities with this entity.
         */
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public byte[] Picture { get; set; }
        public bool IsMentor { get; set; }
        public bool IsMentee { get; set; }
        public string ProfileText { get; set; }
        public bool IsOnline { get; set; }
        public string OnlyFirstName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLogin { get; set; }

        public virtual ICollection<Interest> UndefinedInterests { get; set; }
        public virtual ICollection<Interest> MentorInterests { get; set; }
        public virtual ICollection<Interest> MenteeInterests { get; set; }
        public virtual ICollection<Program> MentorPrograms { get; set; }
        public virtual ICollection<Program> MenteePrograms { get; set; }
        public virtual ICollection<Program> AdminForPrograms { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Notification> NotificationsCreated { get; set; }
        public virtual ICollection<ProgramApplication> ProgramApplications { get; set; }


        public virtual ICollection<Program> CreatorForPrograms { get; set; }
        public virtual ICollection<ProgramMessage> ProgramMessages { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            User p = obj as User;
            if ((System.Object) p == null)
            {
                return false;
            }
            return Id == p.Id;
        }
    }

    public class ApplicationDbContext :
        IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
            /*Disable initializer if we feel like it at some point:  */
            /*Database.SetInitializer<ApplicationDbContext>(null); */
        }

        private DbSet<Interest> Interests { get; set; }
        private DbSet<Program> Programs { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*Initializes the inherited classes (relationships) such as CustomUserLogin,
                                                 *  CustomUserRole & CustomUserClaim*/

            modelBuilder.Entity<Program>()
                .HasRequired<User>(p => p.Creator)
                .WithMany(u => u.CreatorForPrograms)
                .HasForeignKey(p => p.CreatorId).WillCascadeOnDelete(false);
            /*i suppose that a program shouldnt be deleted just because the creator is,
                                                                           but we have to realize that the id, might eventually return null if some1
                                                                            * deletes a profile. But maybe we just keep profiles, and make sure that people
                                                                          can activate them again? kind of like facebook i suppose? */
            
          
            modelBuilder.Entity<ProgramMessage>()
                .HasRequired<User>(pm => pm.User)
                .WithMany(u => u.ProgramMessages)
                .HasForeignKey(pm => pm.UserId).WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramMessage>()
                .HasRequired<Program>(pm => pm.Program)
                .WithMany(u => u.ProgramMessages)
                .HasForeignKey(pm => pm.ProgramId).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany<Interest>(i => i.MenteeInterests)
                .WithMany(a => a.MenteeUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("MenteeInterestRefId");
                    cs.ToTable("UserMenteeInterest");
                });

            modelBuilder.Entity<User>()
                .HasMany<Interest>(i => i.MentorInterests)
                .WithMany(a => a.MentorUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("InterestRefId");
                    cs.ToTable("UserMentorInterest");
                });

            modelBuilder.Entity<User>()
                .HasMany<Interest>(i => i.UndefinedInterests)
                .WithMany(a => a.UndefinedUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("InterestRefId");
                    cs.ToTable("UserUndefinedInterests");
                });

            modelBuilder.Entity<User>()
                .HasMany<Program>(a => a.MentorPrograms)
                .WithMany(p => p.Mentors)
                .Map(cs =>
                {
                    cs.MapLeftKey("MentorRefId");
                    cs.MapRightKey("ProgramRefId");
                    cs.ToTable("MentorProgram");
                });

            modelBuilder.Entity<User>()
                .HasMany<Program>(a => a.MenteePrograms)
                .WithMany(p => p.Mentee)
                .Map(cs =>
                {
                    cs.MapLeftKey("MenteeRefId");
                    cs.MapRightKey("ProgramRefId");
                    cs.ToTable("MenteeProgram");
                });

            modelBuilder.Entity<User>()
                .HasMany<Program>(a => a.MenteePrograms)
                .WithMany(p => p.Mentee)
                .Map(cs =>
                {
                    cs.MapLeftKey("MenteeRefId");
                    cs.MapRightKey("ProgramRefId");
                    cs.ToTable("MenteeProgram");
                });

            modelBuilder.Entity<User>()
                .HasMany<Program>(a => a.AdminForPrograms)
                .WithMany(p => p.Admins)
                .Map(cs =>
                {
                    cs.MapLeftKey("AdminRefId");
                    cs.MapRightKey("ProgramRefId");
                    cs.ToTable("AdminProgram");
                });

            modelBuilder.Entity<Program>()
                .HasRequired<Interest>(p => p.Interest)
                .WithMany(i => i.ProgramInterests)
                .HasForeignKey(p => p.InterestId);

            modelBuilder.Entity<Notification>()
                .HasRequired<Program>(n => n.Program)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.ProgramId);

            modelBuilder.Entity<ProgramApplication>()
                .HasRequired<Program>(pa => pa.ReceiveingProgram)
                .WithMany(p => p.ProgramApplications)
                .HasForeignKey(pa => pa.ReceivingProgramId).WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramApplication>()
                .HasRequired<User>(pa => pa.SendingUser)
                .WithMany(u => u.ProgramApplications)
                .HasForeignKey(pa => pa.SenderId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Notification>()
                .HasRequired<User>(n => n.NotificationCreator)
                .WithMany(u => u.NotificationsCreated)
                .HasForeignKey(n => n.CreatorId).WillCascadeOnDelete(false);
            ;

            modelBuilder.Entity<User>()
                .HasMany<Notification>(u => u.Notifications)
                .WithMany(n => n.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("NotificationRefId");
                    cs.ToTable("NotificationUser");
                });
        }
    

    public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

    public class CustomUserRole : IdentityUserRole<int>
    {
    }

    public class CustomUserClaim : IdentityUserClaim<int>
    {
    }

    public class CustomUserLogin : IdentityUserLogin<int>
    {
    }

    public class CustomRole : IdentityRole<int, CustomUserRole>, IRole<int>
    {
        public string Description { get; set; }

        public CustomRole() : base()
        {
        }

        public CustomRole(string name)
            : this()
        {
            this.Name = name;
        }

        public CustomRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }
    }

/*    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }*/

    public class CustomUserStore : UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    /* public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }*/

    public class CustomRoleStore
        : RoleStore<CustomRole, int, CustomUserRole>,
            IQueryableRoleStore<CustomRole, int>,
            IRoleStore<CustomRole, int>, IDisposable
    {
        public CustomRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public CustomRoleStore(DbContext context)
            : base(context)
        {
        }
    }
}