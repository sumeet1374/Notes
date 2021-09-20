using Microsoft.EntityFrameworkCore;
using Notes.Db.Interfaces;
using Notes.Db.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notes.Db.Implementation
{
    public class NotesDao : INotesDao
    {
        private const int DEFAULT_PAGE_SIZE = 10;
        public NotesDbContext Context { get ; set ; }

        public async Task<int> Create(NoteInfo entity)
        {
            Context.Notes.Add(entity);
            var result = await Context.SaveChangesAsync();
            return result;
        }

        public async Task Delete(int id)
        {
           var note=  Context.Notes.FirstOrDefault((note) => note.Id == id);
            if(note != null)
            {
                Context.Notes.Remove(note);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<NoteInfo> Get(int id)
        {
            var result = await Context.Notes.FirstOrDefaultAsync((note) => note.Id == id);
            return result;
        }

        public async Task<PagedModel<NoteInfo>> GetAll(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize < 0)
                pageSize = DEFAULT_PAGE_SIZE;

            int count = Context.Notes.Count();
            int totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

            if (pageNumber > totalPages)
                pageNumber = 1;


            var result = await Context.Notes.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
            return new PagedModel<NoteInfo>() { Result = result, PageNumber = pageNumber, PageSize = pageSize, TotalPages = totalPages, TotalRecords = count };
        }

        public async Task<NoteInfo> Query(Expression<Func<NoteInfo, bool>> queryCondition)
        {
            var result = await Context.Notes.FirstOrDefaultAsync(queryCondition);
            return result;
        }

        public async  Task<PagedModel<NoteInfo>> QueryList(Expression<Func<NoteInfo, bool>> queryCondition, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize < 0)
                pageSize = DEFAULT_PAGE_SIZE;

            int count = Context.Notes.Where(queryCondition).Count();
            int totalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);

            if (pageNumber > totalPages)
                pageNumber = 1;


            var result = await Context.Notes.Where(queryCondition).Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(x => x.Id).ToListAsync();
            return new PagedModel<NoteInfo>() { Result = result, PageNumber = pageNumber, PageSize = pageSize, TotalPages = totalPages, TotalRecords = count };
        }
    }
}
