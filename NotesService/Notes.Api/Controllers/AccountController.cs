using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Auth;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System.Threading.Tasks;


namespace Notes.Api.Controllers
{
    /// <summary>
    ///  Account/user management REST Apis
    /// </summary>
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
        [Authorize(policy: AuthPolicies.USER)]
        public async Task<User> GetAuthenticatedUserByExternalUd()
        {
            var userId = User.Identity.Name;
            var user = await service.GetUserByExternalId(userId);
            return user;
         
        }

        [HttpPost]
        [Route("/users")]
        public async Task<ActionResult> CreateUser(User user)
        {

            await service.CreateUser(user);
            return Ok();
          
        }


        [HttpPost]
        [Route("/adminusers")]
        [Authorize(policy: AuthPolicies.ADMIN)]
        public async Task<ActionResult> CreateUserByAdmin(User user)
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
