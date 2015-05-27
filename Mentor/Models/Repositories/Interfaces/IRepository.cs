using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentor.Models.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetDatabase();
        Boolean IsDatabaseEmpty();
        int Create(TEntity Object);
        TEntity Read(int? id);
        void Update(TEntity Object);
        void Delete(TEntity Object);
    }
}
