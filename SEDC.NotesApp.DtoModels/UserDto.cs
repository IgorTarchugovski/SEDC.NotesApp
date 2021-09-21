using SEDC.NotesApp.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.DtoModels
{
    public class UserDto
    {
        public UserDto()
        {
            Notes = new List<NoteDto>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
