using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Shared.Exceptions
{
    public class NoteException : Exception
    {
        public int? NoteId { get; set; }
        public int UserId { get; set; }

        public NoteException() : base("There has been an issue with the note")
        {

        }

        public NoteException(int? noteId, int userId)
        {
            NoteId = noteId;
            UserId = userId;
        }

        public NoteException(int? noteId, int userId, string message) : base(message)
        {
            NoteId = noteId;
            UserId = userId;
        }
    }
}
