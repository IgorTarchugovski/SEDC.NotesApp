using Dapper;
using Dapper.Contrib.Extensions;
using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.DataAccess.Dapper
{
    public class DapperUserRepository : IRepository<User>
    {
        private readonly string _connectionString;
        public DapperUserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Insert(entity);
            }
        }
        public List<User> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<User> users = new List<User>();
                List<Note> notes = new List<Note>();

                using (var multi = connection.QueryMultiple("SELECT *  FROM [Users]; SELECT * FROM [Notes]"))              
                {
                    users = multi.Read<User>().ToList();
                    notes = multi.Read<Note>().ToList();
                    foreach (var user in users)
                    {
                        foreach (var note in notes)
                        {
                            if (user.Id == note.UserId)
                            {
                                user.Notes.Add(note);
                            }
                        }
                    }
                }
                return users;
            }
        }

        public User GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                User user = new User();
                // Approach 1
                string sql = "exec dbo.spUsers_GetById @id; exec dbo.spNotes_GetByUserId @userId";

                // Approach 2
                //string sql = "dbo.spUsers_GetById; dbo.spNotes_GetByUserId";

                using (var multi = connection
                    .QueryMultiple(sql, new { id = id, userId = id}))
                {
                    user = multi.Read<User>().Where(user => user.Id == id).Single();
                    //user = multi.Read<User>().Single();
                    List<Note> userNotes = multi.Read<Note>().ToList();
                    user.Notes = userNotes;
                }
                return user;
            }
        }

        public void Remove(int id)
        {
            User user = GetById(id);
            if (user == null) return;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Delete(user);
            }
        }

        public void Update(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Update(entity);
            }
        }
    }
}
