using SEDC.NotesApp.Helpers.Mappers;
using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Models.DtoModels;
using SEDC.NotesApp.Repositories;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepo;
        public NoteService(IRepository<Note> noteRepo)
        {
            _noteRepo = noteRepo;
        }
        public string AddNewNote(NoteDto note)
        {
            if (string.IsNullOrEmpty(note.Text) || string.IsNullOrEmpty(note.Color))
            {
                throw new BadImageFormatException("Text and color fields are required");
            }
            Note noteDb = NoteMapper.MapNoteDtoToNoteModel(note);
            _noteRepo.Add(noteDb);
            return "Note was created succesfully";
        }

        public List<NoteDto> GetAllNotes()
        {
            return _noteRepo.GetAll()
                .Select(note => NoteMapper.MapNoteToNoteDtoModel(note)).ToList();
        }

        public List<NoteDto> GetAllUserNotes(int userId)
        {
            return _noteRepo.GetAll()
                .Where(note => note.UserId == userId)
                .Select(note => NoteMapper.MapNoteToNoteDtoModel(note))
                .ToList();
        }

        public string RemoveNote(int noteId)
        {
            Note note = _noteRepo.GetById(noteId);
            if (note == null)
            {
                throw new NoteException(noteId, 0, "No such note");
            }
            _noteRepo.Remove(noteId);
            return "Note deleted succesfully";
        }

        public string UpdateNote(NoteDto note)
        {
            Note noteDb = _noteRepo.GetById(note.Id);
            if (noteDb == null)
            {
                throw new NoteException(note.Id, note.UserId, "No such note");
            }
            Note mappedNote = NoteMapper.MapNoteDtoToNoteModel(note);
            _noteRepo.Update(mappedNote);
            return "Note updated succesfully";
        }
    }
}
