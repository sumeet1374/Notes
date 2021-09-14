using Notes.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Interfaces
{
    public interface IUserDao: IBaseDao
    {
        Task<PagedModel<User>> GetAllUsers(int pageNumber, int pageSize);
        Task<int> CreateUser(User user);
        Task<User> GetUser(int id);
        Task<User> QueryUser(Expression<Func<User, bool>> queryCondition);
    }
}
