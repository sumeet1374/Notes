using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Auth;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Notes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService service;

        public AccountController(IUserService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("/users/{id}")]
        [Authorize(policy: AuthPolicies.ADMIN)]
        public async Task<User>  GetUser(int id)
        {
            var user =  await service.GetUser(id);
            return user;
        }
        [HttpGet]
        [Route("/users/extId")]
        [Authorize(policy: AuthPolicies.ADMIN)]
        public async Task<User> GetAuthenticatedUserByExternalUd()
        {
            var userId = User.Identity.Name;
            var user = await service.GetUserByExternalId(userId);
            return user;
        }

        [HttpPost]
        [Route("/users")]
        public async Task<ActionResult> CreateUaser(User user)
        {

            await service.CreateUser(user);
            return Ok();
          
        }

       

        [HttpGet]
        [Route("/users")]
        [Authorize(policy: AuthPolicies.ADMIN)]
        public async Task<ActionResult<PagedModel<User>>> GetAllUsers(int pageNumber,int pageSize)
        {
            var usr = User.Identity.Name;
            var result = await service.GetAllUsers(pageNumber, pageSize);
            return CreatedAtAction(nameof(GetAllUsers), result);
        }

       
        
    }
}
