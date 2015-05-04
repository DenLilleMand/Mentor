using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Mentor.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
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


        public virtual ICollection<Interest> Interests { get; set; }
        

        public virtual Mentee Mentee { get; set; }

        public virtual Mentor Mentor { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>("DefaultConnection"));
            /*Disable initializer if we feel like it at some point:  */
            /*Database.SetInitializer<ApplicationDbContext>(null); */
        }

        DbSet<Interest> InterestDbSet { get; set; }
        DbSet<Program> ProgramDbSet { get; set; }
 /*       DbSet<Mentor> MentorDbSet { get; set; }
        DbSet<Mentee> MenteeDbSet { get; set; }*/



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); /*Initializes the inherited classes (relationships) such as CustomUserLogin,
                                                 *  CustomUserRole & CustomUserClaim*/



            /* Many-to-many between Mentor and Program, One mentor can be part of many programs, 
             and a program can have many mentors part of it.*/
            modelBuilder.Entity<Mentor>()
                        .HasMany<Program>(p => p.Programs)
                        .WithMany(m => m.Mentors)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("MentorRefId");
                            cs.MapRightKey("ProgramRefId");
                            cs.ToTable("MentorPrograms");
                        });

            /* Many-to-many between Mentee and Program, One mentee can be part of many programs, 
             * and a program can have many mentees part of it.*/
            modelBuilder.Entity<Mentee>()
                        .HasMany<Program>(p => p.Programs)
                        .WithMany(m => m.Mentees)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("MenteeRefId");
                            cs.MapRightKey("ProgramRefId");
                            cs.ToTable("MenteePrograms");
                        });

            /* one-to-many between Interest and Program, one interest can have many programs, but one 
             * program can only have one interest.*/
            modelBuilder.Entity<Program>()
                        .HasRequired<Interest>(i => i.Interest)
                        .WithMany(p => p.Programs)
                        .HasForeignKey(i => i.InterestId);

            /* Many-to-many relationship between ApplicationUser and Interests. One ApplicationUser can 
             * have many Interests, and one Interest can be gained by many ApplicationUsers */
            modelBuilder.Entity<ApplicationUser>()
                      .HasMany<Interest>(i => i.Interests)
                      .WithMany(a => a.ApplicationUsers)
                      .Map(cs =>
                      {
                          cs.MapLeftKey("ApplicationUserRefId");
                          cs.MapRightKey("InterestRefId");
                          cs.ToTable("ApplicationUserInterest");
                      });

/*             modelBuilder.Entity<Mentee>().HasKey(m => m.MenteeId);
             modelBuilder.Entity<Mentor>().HasKey(m => m.MentorId);*/
/*
            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(m => m.Mentor);

            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(m => m.Mentee);*/


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

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
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