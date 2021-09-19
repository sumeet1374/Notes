using Notes.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Interfaces
{
    public interface IBaseDao<T>
    {
         NotesDbContext Context { get; set; }
        Task<PagedModel<T>> GetAll(int pageNumber, int pageSize);
        Task<int> Create(T entity);
        Task<T> Get(int id);
        Task<T> Query(Expression<Func<T, bool>> queryCondition);
    }
}
