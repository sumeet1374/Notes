using System;

namespace Notes.Db.Model
{
    /// <summary>
    ///  Notes Entity
    /// </summary>
    public class NoteInfo
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
