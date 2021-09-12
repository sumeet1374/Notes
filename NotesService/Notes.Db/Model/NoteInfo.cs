using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Model
{
    public class NoteInfo
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }

    }
}
