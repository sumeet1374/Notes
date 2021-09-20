using Notes.Db;
using Notes.Db.Interfaces;
using Notes.Services.Interfaces;
using Notes.Services.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Services.Implementation
{
    public class NotesService : INotesService
    {
        private readonly INotesDao dao;
        private readonly IUserDao userDao;
        private readonly IDbProvider provider;



        public NotesService(INotesDao dao, IUserDao userDao, IDbProvider provider)
        {
            this.dao = dao;
            this.userDao = userDao;
            this.provider = provider;
        }

        public async Task<int> CreateNote(NoteInfo note)
        {
            using (var context = new NotesDbContext(provider))
            {

                dao.Context = context;
                userDao.Context = context;

                var user = await userDao.Query((user) => user.ExternalUserId == note.UserIdentifier);
                if (user == null)
                    throw new ValidationException("User not found or authorized");

                var dbNote = new Notes.Db.Model.NoteInfo();
                dbNote.Note = note.Note;
                dbNote.UserId = user.Id;
                dbNote.CreatedDate = DateTime.Now;

                int id = await dao.Create(dbNote);
                return id;
            }
        }

        public async Task DeleteNote(int noteId)
        {
            using (var context = new NotesDbContext(provider))
            {
                dao.Context = context;
               await dao.Delete(noteId);
            }
        }

        public async Task<PagedModel<NoteInfo>> GetAllNotes(string externalUserId, int pageNumber, int pageSize)
        {
            using (var context = new NotesDbContext(provider))
            {

                dao.Context = context;
                userDao.Context = context;

                var user = await userDao.Query((user) => user.ExternalUserId == externalUserId);
                if (user == null)
                    throw new ValidationException("User not found or authorized");

                var result = await dao.QueryList((note) => note.UserId == user.Id,pageNumber,pageSize);
                var model = new PagedModel<NoteInfo>();
                model.PageNumber = result.PageNumber;
                model.PageSize = result.PageSize;
                model.TotalPages = result.TotalPages;
                model.TotalRecords = result.TotalRecords;
                model.Result = result.Result.Select(x => new NoteInfo(x)).ToList();
                return model;
            }
        }

        public async Task<NoteInfo> GetNote(int id)
        {
            using (var context = new NotesDbContext(provider))
            {
                dao.Context = context;
               var result = await dao.Get(id);
               return new NoteInfo(result);
            }
        }
    }
}
