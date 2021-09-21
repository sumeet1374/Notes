using Microsoft.AspNetCore.Authorization;
using Notes.Services.Interfaces;
using System.Threading.Tasks;

namespace Notes.Api.Auth
{
    /// <summary>
    ///  Authorization Policy
    ///  Check IDP user id in User Db
    ///  Authorize only if Db User is Active or found in the database
    /// </summary>
    public class NotesAuthorizationHandler : AuthorizationHandler<NotesAuthRequirement>
    {
        private readonly IUserService service;

        /// <summary>
        ///  Constructor , Inject user service
        /// </summary>
        /// <param name="service"></param>
        public NotesAuthorizationHandler(IUserService service)
        {
            this.service = service;
        }
        /// <summary>
        ///  Authroization handler
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotesAuthRequirement requirement)
        {
            var userName = context.User.Identity.Name;

            if (userName != null)
            {
               var user = service.GetUserByExternalId(userName).Result;
                if(requirement.IsAdmin)
                {
                    if (user != null && user.Active && user.IsAdmin == requirement.IsAdmin)
                        context.Succeed(requirement);
                }
                else
                {
                    if (user != null && user.Active)
                        context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
