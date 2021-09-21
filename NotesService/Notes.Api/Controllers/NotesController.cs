using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Auth;
using Notes.Api.Model;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System.Threading.Tasks;

namespace Notes.Api.Controllers
{
    /// <summary>
    ///  Notes Managment REST API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService service;

        public NotesController(INotesService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("/notes")]
        [Authorize(policy:AuthPolicies.USER)]
        public async Task<PagedModel<NoteInfo>> GetNotes(int pageNumber,int pageSize)
        {
            var result = await service.GetAllNotes(User.Identity.Name, pageNumber, pageSize);
            return result;
        }

        [HttpGet]
        [Route("/notes/{id}")]
        [Authorize(policy: AuthPolicies.USER)]
        public async Task<NoteInfo> GetNote(int id)
        {
            var result = await service.GetNote(id);
            return result;
        }
        [HttpPost]
        [Route("/notes")]
        [Authorize(policy: AuthPolicies.USER)]
        public async Task<ActionResult>  CreateNote(NoteInput input)
        {
             await service.CreateNote(new NoteInfo() { Note = input.Note, UserIdentifier = User.Identity.Name });
            return Ok();
        }

        [HttpPost]
        [Route("/notes/remove")]
        public async Task<ActionResult> DeleteNote(NoteInput input)
        {
            await service.DeleteNote(input.Id);
            return Ok();
        }
    }
}
