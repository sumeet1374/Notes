using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Model
{
    /// <summary>
    ///  DTO class for user
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        // Mapped User Id from IDP (Identity Provider)
        public string ExternalUserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
        public bool Active { get; set; }

        public User()
        {

        }

        public User(Notes.Db.Model.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            ExternalUserId = user.ExternalUserId;
            IsAdmin = user.IsAdmin;
            Active = user.Active;


        }
    }
}
