using Notes.Db.Model;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Notes.Db.Interfaces
{
    public interface INotesDao:IBaseDao<NoteInfo>
    {
        Task<PagedModel<NoteInfo>> QueryList(Expression<Func<NoteInfo, bool>> queryCondition,int pageNumnber,int pageSize);
        Task Delete(int id);
    }
}
