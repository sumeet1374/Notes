using Notes.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Interfaces
{
    public interface IUserDao: IBaseDao<User>
    {
       
    }
}
