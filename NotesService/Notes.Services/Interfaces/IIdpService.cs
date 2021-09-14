using Notes.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Interfaces
{
    /// <summary>
    ///  Interfaxe for IDP (Identity Priovider) service
    /// </summary>
    public interface IIdpService
    {
        public Task CreateUser(User user);
     
    }
}
