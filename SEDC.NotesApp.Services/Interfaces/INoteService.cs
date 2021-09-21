using SEDC.NotesApp.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteDto> GetAllNotes();
        List<NoteDto> GetAllUserNotes(int userId);
        string AddNewNote(NoteDto note);
        string RemoveNote(int noteId);
        string UpdateNote(NoteDto note);
    }
}
