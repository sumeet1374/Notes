using Microsoft.EntityFrameworkCore;
using Notes.Db.Interfaces;
using Notes.Db.Model;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notes.Db.Implementation
{
    /// <summary>
    ///  User related Data Access Object
    /// </summary>
    public class UserDao : IUserDao
    {
        
        private const int DEFAULT_PAGE_SIZE = 10;

        public NotesDbContext Context { get; set; }

        public UserDao()
        {
            
        }

        public async Task<int> Create(User user)
        {

            Context.Users.Add(user);
            var result = await Context.SaveChangesAsync();
            return result;

        }

        public async Task<PagedModel<User>> GetAll(int pageNumber, int pageSize)
        {

            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize < 0)
                pageSize = DEFAULT_PAGE_SIZE;

            int count = Context.Users.Count();
            int totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

            if (pageNumber > totalPages)
                pageNumber = 1;


            var result = await Context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x=>x.Email).ToListAsync();
            return new PagedModel<User>() { Result = result, PageNumber = pageNumber, PageSize = pageSize, TotalPages = totalPages, TotalRecords = count };
        }

        public async Task<User> Get(int id)
        {
            var result = await Context.Users.FirstOrDefaultAsync((user) => user.Id == id);
            return result;
        }

      
        /// <summary>
        ///  Get the first user based upon the query condition
        /// </summary>
        /// <param name="queryCondition"></param>
        /// <returns></returns>
        public async Task<User> Query(Expression<System.Func<User, bool>> queryCondition)
        {
            var result = await Context.Users.FirstOrDefaultAsync(queryCondition);
            return result;
        }
    }
}
