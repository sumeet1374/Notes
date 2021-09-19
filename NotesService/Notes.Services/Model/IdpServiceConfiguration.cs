using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Data required to establish connection with IDP
    /// </summary>
    public class IdpServiceConfiguration
    {
        public string UserServiceClientId { get; set; }
        public string UserServiceClientSecret { get; set; }
        public string UserServiceBaseUrl { get; set; }
        public string UserServiceAudience { get; set; }
    }
}
