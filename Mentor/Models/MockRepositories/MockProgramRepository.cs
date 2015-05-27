using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mentor.Models.Repositories.Interfaces;

namespace Mentor.Models.MockRepositories
{
    public class MockProgramRepository : IRepository<Program>
    {
        public IEnumerable<Program> GetDatabase()
        {
            throw new NotImplementedException();
        }

        public bool IsDatabaseEmpty()
        {
            throw new NotImplementedException();
        }

        public int Create(Program Object)
        {
            return 0;
        }

        public Program Read(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(Program Object)
        {
            throw new NotImplementedException();
        }

        public void Delete(Program Object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Program> Search(string search)
        {
            throw new NotImplementedException();
        }


        public List<ProgramMessage> GetMessages(int? id)
        {
            throw new NotImplementedException();
        }
    }
}