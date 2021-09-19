using Microsoft.AspNetCore.Authorization;
using Notes.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Api.Auth
{
    /// <summary>
    ///  Authorization Policyu
    ///  Check IDP user id in User Db
    ///  Authorize only if Db User is Active or found in the database
    /// </summary>
    public class NotesAuthorizationHandler : AuthorizationHandler<NotesAuthRequirement>
    {
        private readonly IUserService service;

        public NotesAuthorizationHandler(IUserService service)
        {
            this.service = service;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotesAuthRequirement requirement)
        {
            var userName = context.User.Identity.Name;

            if (userName != null)
            {
               var user = service.GetUserByExternalId(userName).Result;
                if (user != null && user.Active && user.IsAdmin == requirement.IsAdmin)
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
