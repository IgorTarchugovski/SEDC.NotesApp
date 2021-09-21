using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Shared.Exceptions
{
    public class UserException : Exception
    {
        public int? UserId { get; set; }

        public UserException() : base("There was an issue with the user")
        {

        }

        public UserException(int? userId) 
        {
            UserId = userId;
        }

        public UserException(int? userId, string message) : base(message)
        {
            UserId = userId;
        }
    }
}
