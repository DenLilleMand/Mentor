using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.Repositories.Concrete_Implementation
{
    public class ProgramRepository : IRepository<Program>
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<Program> GetDatabase()
        {
            return db.Set<Program>();
        }

        public bool IsDatabaseEmpty()
        {
            return db.Set<Program>().Any();
        }

        public void Create(Program Object)
        {
            db.Set<Program>().Add(Object);
            db.SaveChanges();
        }

        public Program Read(int? id)
        {
            return db.Set<Program>().Find(id);
        }

        public void Update(Program Object)
        {
            db.Entry(Object).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Program Object)
        {
            db.Entry(Object).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public IEnumerable<Program> Search(string search)
        {
            
            return db.Set<Program>().Where(n => n.Name.Contains(search));
        }
    }
}