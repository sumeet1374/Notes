using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Api.Auth
{
    /// <summary>
    ///  Marker class for Policy
    /// </summary>
    public class NotesAuthRequirement: IAuthorizationRequirement
    {
        public bool IsAdmin { get; set; }
    }
}
