using Microsoft.EntityFrameworkCore;
using Notes.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db
{
    public class NotesDbContexzt:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<NoteInfo> Notes { get; set; }

        public NotesDbContexzt()
        {

        }
    }
}
