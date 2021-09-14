using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Interfaces
{
    public interface IBaseDao
    {
         NotesDbContext Context { get; set; }
    }
}
