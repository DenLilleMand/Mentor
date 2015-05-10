using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.Repositories.Concrete_Implementation
{
    public class UserRepository : IRepository<User>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<User> GetDatabase()
        {
            return db.Users;
        }

        public bool IsDatabaseEmpty()
        {
            return db.Users.Any();
        }

        public void Create(User Object)
        {
            db.Users.Add(Object);
            db.SaveChanges();
        }

        public User Read(int? id)
        {
            User s = new User();
            s = db.Users.Find(id);

           return s;
            
        }

        public void Update(User Object)
        {
            db.Entry(Object).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(User Object)
        {
            db.Entry(Object).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public IEnumerable<User> Search(string search)
        {
           // IEnumerable<User> test = 
            return db.Users.Where(n => n.FirstName.Contains(search) || n.LastName.Contains(search));
        }
    }
}