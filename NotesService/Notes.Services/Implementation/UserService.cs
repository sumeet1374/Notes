using Notes.Db;
using Notes.Db.Interfaces;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserDao userDao;
        private readonly IIdpService idpService;
        private readonly IDbProvider provider;

        public UserService(IUserDao userDao, IIdpService idpService, IDbProvider provider)
        {
            this.userDao = userDao;
            this.idpService = idpService;
            this.provider = provider;
        }

        /// <summary>
        ///  Create Db User from Service Model Usaer
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Notes.Db.Model.User CreateOrUpdateDbUser(User user, Notes.Db.Model.User dbUser = null)
        {
            if (dbUser == null)
                dbUser = new Notes.Db.Model.User();
            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.IsAdmin = user.IsAdmin;
            dbUser.Active = true;
            return dbUser;
        }

        /// <summary>
        ///  Service to create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<int> CreateUser(User user)
        {
            try
            {
                // Create user in IDP
                await idpService.CreateUser(user);
                // Create user in local db
                using (var context = new NotesDbContext(provider))
                {
                    userDao.Context = context;
                    //Check if Email Exists
                    var existingUser = await userDao.QueryUser((x) => string.Equals(x.Email, user.Email, StringComparison.OrdinalIgnoreCase));
                    if (existingUser != null)
                    {
                        // Update User
                        var userToUpdate = CreateOrUpdateDbUser(user, existingUser);
                        context.SaveChanges();
                        return existingUser.Id;

                    }
                    else
                    {
                        var result = await userDao.CreateUser(CreateOrUpdateDbUser(user));
                        return result;
                    }
                }

            }
            catch (IdpWebException webException)
            {
                if (webException.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new ValidationException($"{user.Email} already registered.");
                }

                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedModel<User>> GetAllUsers(int pageNumber, int pageSize)
        {
            using (var context = new NotesDbContext(provider))
            {
                userDao.Context = context;

                var result = await userDao.GetAllUsers(pageNumber, pageSize);

                var pagedResult = new PagedModel<User>();
                pagedResult.PageNumber = result.PageNumber;
                pagedResult.PageSize = result.PageSize;
                pagedResult.TotalPages = result.TotalPages;
                pagedResult.TotalRecords = result.TotalRecords;
                pagedResult.Result = result.Result.Select((data) => new User() { Id = data.Id, FirstName = data.FirstName, LastName = data.LastName, Email = data.Email, ExternalUserId = data.ExternalUserId, IsAdmin = data.IsAdmin, Active = data.Active }).ToList();
                return pagedResult;
            }
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByExternalId(string externalId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}