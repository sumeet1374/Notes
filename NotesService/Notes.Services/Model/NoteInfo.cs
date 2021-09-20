using System;

namespace Notes.Services.Model
{
    /// <summary>
    ///  Note Model
    /// </summary>
    public class NoteInfo
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public int UserId { get; set; }
        public string UserIdentifier { get; set; }
        public DateTime CreatedDate { get; set; }

        public NoteInfo()
        {

        }

        public NoteInfo(Notes.Db.Model.NoteInfo note)
        {
            Id = note.Id;
            Note = note.Note;
            UserId = note.UserId;
            CreatedDate = note.CreatedDate;
            
        }
    }
}
