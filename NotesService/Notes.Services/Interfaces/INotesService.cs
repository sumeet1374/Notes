using Notes.Services.Model;
using System.Threading.Tasks;

namespace Notes.Services.Interfaces
{
    public interface INotesService
    {
        Task<int> CreateNote(NoteInfo note);
        Task DeleteNote(int noteId);
        Task<PagedModel<NoteInfo>> GetAllNotes(string externalUserId,int pageNumber,int pageSize);
        Task<NoteInfo> GetNote(int id);
    }
}
