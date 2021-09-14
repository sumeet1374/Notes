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
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string IdpServiceBaseUrl { get; set; }
        public string Audience { get; set; }
    }
}
