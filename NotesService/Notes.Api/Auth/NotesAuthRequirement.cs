using Microsoft.AspNetCore.Authorization;

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
