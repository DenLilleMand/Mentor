using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mentor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
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

        public virtual ICollection<Interest> UndefinedInterests { get; set; }
        public virtual ICollection<Interest> MentorInterests { get; set; }
        public virtual ICollection<Interest> MenteeInterests { get; set; } 
        public virtual ICollection<Program> MentorPrograms { get; set; }
        public virtual ICollection<Program> MenteePrograms { get; set; }
        public virtual ICollection<Program> AdminForPrograms { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
            /*Disable initializer if we feel like it at some point:  */
            /*Database.SetInitializer<ApplicationDbContext>(null); */
        }

        DbSet<Interest> Interests   { get; set; }
        DbSet<Program> Programs     { get; set; }
       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); /*Initializes the inherited classes (relationships) such as CustomUserLogin,
                                                 *  CustomUserRole & CustomUserClaim*/


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

        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}