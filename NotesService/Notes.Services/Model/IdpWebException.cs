using System;
using System.Net;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Special exception class to capture IDP errors
    /// </summary>
    public class IdpWebException:Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public IdpWebException(HttpStatusCode code,string message):base(message)
        {
            StatusCode = code;
        }
    }
}
