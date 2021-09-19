using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Api.Model
{
    /// <summary>
    ///  Note input
    /// </summary>
    public class NoteInput
    {
        public string Note { get; set; }
        public string UserIdentifier { get; set; }
    }
}
