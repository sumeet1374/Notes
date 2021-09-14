using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Services.Model;

namespace Notes.Services.Interfaces
{
    /// <summary>
    ///  Service Interface to manage users
    /// </summary>
    public interface IUserService
    {
        Task<PagedModel<User>> GetAllUsers(int pageNumber, int pageSize);
        Task<int> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> GetUser(int id);
        Task<User> GetUserByExternalId(string externalId);
        Task<User> GetUserByEmail(string email);

    }
}
