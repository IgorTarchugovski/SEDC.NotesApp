using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Models.DtoModels;
using SEDC.NotesApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Helpers.Mappers
{
    public static class NoteMapper
    {
        public static Note MapNoteDtoToNoteModel(NoteDto noteDto)
        {
            return new Note()
            {
                Id = noteDto.Id,
                Text = noteDto.Text,
                Color = noteDto.Color,
                Tag = (int)noteDto.Tag,
                UserId = noteDto.UserId
            };
        }

        public static NoteDto MapNoteToNoteDtoModel(Note note)
        {
            return new NoteDto()
            {
                Id = note.Id,
                Text = note.Text,
                Color = note.Color,
                Tag = (Tag)note.Tag,
                UserId = note.UserId
            };
        }
    }
}
