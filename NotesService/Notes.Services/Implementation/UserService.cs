using Notes.Db;
using Notes.Db.Interfaces;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
        ///  Validate
        /// </summary>
        /// <param name="user"></param>
        private void ValidateUserCreationInput(User user)
        {
            string emailRegx = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            Regex re = new Regex(emailRegx);

            if (string.IsNullOrEmpty(user.Email))
                throw new ValidationException(string.Format(NotesServiceMessages.REQUIRED,nameof(user.Email)));

            if (!re.IsMatch(user.Email))
                throw new ValidationException(string.Format(NotesServiceMessages.INVALID, nameof(user.Email)));

            if(string.IsNullOrEmpty(user.Password))
                throw new ValidationException(string.Format(NotesServiceMessages.REQUIRED, nameof(user.Password)));

            if(user.Password.Length < 8)
                throw new ValidationException(string.Format(NotesServiceMessages.MIN_LENGTH, nameof(user.Password),8));

            if (string.IsNullOrEmpty(user.ConfirmPassword))
                throw new ValidationException(string.Format(NotesServiceMessages.REQUIRED, nameof(user.ConfirmPassword)));
            
      

            if (user.Password != user.ConfirmPassword)
                throw new ValidationException(string.Format(NotesServiceMessages.MATCH, nameof(user.Password), nameof(user.ConfirmPassword)));
        }

        /// <summary>
        ///  Service to create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<int> CreateUser(User user)
        {
            ValidationException except = null;
            IdpResponse idpResponse = null;
            ValidateUserCreationInput(user); // Basic validation
            try
            {
                // Create user in IDP
                idpResponse =  await idpService.CreateUser(user);


            }
            catch (IdpWebException webException)
            {
                throw;

             
            }
            catch(ValidationException ex)
            {
                except = ex;
                idpResponse = ex.MetaData as IdpResponse;
            }
            catch (Exception)
            {
                throw;
            }

            if (idpResponse == null)
                throw new Exception("Uable to save record in IDP");
              
            int result;

            // Create/Sync user in local db
            using (var context = new NotesDbContext(provider))
            {
                userDao.Context = context;
                //Check if Email Exists
                var existingUser = await userDao.Query((x) => x.Email.ToLower() == user.Email);
                if (existingUser != null)
                {
                    // Update User
                    var userToUpdate = CreateOrUpdateDbUser(user, existingUser);
                    userToUpdate.ExternalUserId = idpResponse.user_id;
                    context.SaveChanges();
                    result = existingUser.Id;

                }
                else
                {
                    var userToCreate = CreateOrUpdateDbUser(user);
                    userToCreate.ExternalUserId = idpResponse.user_id;
                    result = await userDao.Create(CreateOrUpdateDbUser(user));
                }
            }

            if (except != null)
                throw except;

            return result;
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

                var result = await userDao.GetAll(pageNumber, pageSize);

                var pagedResult = new PagedModel<User>();
                pagedResult.PageNumber = result.PageNumber;
                pagedResult.PageSize = result.PageSize;
                pagedResult.TotalPages = result.TotalPages;
                pagedResult.TotalRecords = result.TotalRecords;
                pagedResult.Result = result.Result.Select((data) => new User(data)).ToList();
                return pagedResult;
            }
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUser(int id)
        {
            using (var context = new NotesDbContext(provider))
            {
                userDao.Context = context;
                var user = await userDao.Query(x => x.Active && x.Id == id);
                if (user != null)
                    return new User(user);
            }

            return null;
        }

        public async Task<User> GetUserByExternalId(string externalId)
        {
            using (var context = new NotesDbContext(provider))
            {
                userDao.Context = context;
                var user = await userDao.Query(x => x.Active && x.ExternalUserId == externalId);
                if (user != null)
                    return new User(user);
            }

            return null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            using (var context = new NotesDbContext(provider))
            {
                userDao.Context = context;
                var user = await userDao.Query(x => x.Active && x.Email == email);
                if (user != null)
                    return new User(user);
            }

            return null;
        }
    }
}