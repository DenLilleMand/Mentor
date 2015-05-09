using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Models.Repositories.Interfaces
{
    interface IRepository<T>
    {

        IEnumerable<T> GetDatabase();
        Boolean IsDatabaseEmpty();
        void Create(T Object);
        T Read(int? id);
        void Update(T Object);
        void Delete(T Object);
        
        IEnumerable<T> Search(string search);
       
    }
}
