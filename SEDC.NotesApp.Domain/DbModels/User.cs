using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Models.DbModels
{
    public class User
    {
        public User()
        {
            Notes = new List<Note>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Computed] // Not in the Database (Ova vo data bazata ne treba da bide tretirano kako kolona)
        public virtual List<Note> Notes { get; set; }
    }
}
