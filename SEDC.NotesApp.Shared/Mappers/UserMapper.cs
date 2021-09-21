using SEDC.NotesApp.DtoModels;
using SEDC.NotesApp.Helpers.Mappers;
using SEDC.NotesApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.Shared.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserDto user)
        {
            return new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Notes = user.Notes.Select(note => NoteMapper.MapNoteDtoToNoteModel(note)).ToList()
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Notes = user.Notes.Select(note => NoteMapper.MapNoteToNoteDtoModel(note)).ToList()
            };
        }
    }
}
