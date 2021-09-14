using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Class for Idp User
    /// </summary>
    public class IdpUser
    {
        private const string CONNECTION = "Username-Password-Authentication";
        public string Email { get; set; }
        public bool Verify_email { get; set; } = false;
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public string Name { get; set; }
        public string Connection { get; set; } = CONNECTION;
        public string Password { get; set; }
    }
}
