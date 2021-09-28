using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.NotesApp.DataAccess.AdoNet
{
    public class AdoNetUserRepository : IRepository<User>
    {
        private readonly string _connectionString;
        public AdoNetUserRepository(string connectiongString)
        {
            _connectionString = connectiongString;
        }
        public void Add(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO [Users] (UserName, Password, FirstName, LastName) " +
                        $"VALUES(@userName, @password, @firstName, @lastName)";
                    command.Parameters.AddWithValue("@userName", entity.UserName);
                    command.Parameters.AddWithValue("@password", entity.Password);
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            List<Note> notes = new List<Note>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [Users]";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        // approach 1
                        /*User user = new User()
                        {
                            Id = dataReader.GetInt32(0),
                            UserName = dataReader.GetString(1),
                            Password = dataReader.GetString(2),
                            FirstName = dataReader.GetString(3),
                            LastName = dataReader.GetString(4)
                        };*/

                        // approach 2
                        /*User user = new User()
                        {
                            Id = dataReader.GetFieldValue<int>(0),
                            UserName = dataReader.GetFieldValue<string>(1),
                            Password = dataReader.GetFieldValue<string>(2),
                            FirstName = dataReader.GetFieldValue<string>(3),
                            LastName = dataReader.GetFieldValue<string>(4)
                        };*/

                        // approach 3
                        User user = new User()
                        {
                            Id = (int)dataReader["Id"],
                            UserName = (string)dataReader["UserName"],
                            Password = (string)dataReader["Password"],
                            FirstName = (string)dataReader["FirstName"],
                            LastName = (string)dataReader["LastName"]
                        };
                        users.Add(user);
                    }
                    dataReader.Close();
                }

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [Notes]";
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = (int)dataReader["Id"],
                            Text = (string)dataReader["Text"],
                            Color = (string)dataReader["Color"],
                            Tag = (int)dataReader["Tag"],
                            UserId = (int)dataReader["UserId"]
                        };
                        // Example
                        /*User user = users.FirstOrDefault(user => user.Id = note.UserId);*/
                        notes.Add(note);
                    }
                    dataReader.Close();
                }

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

        public User GetById(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                User user = new User();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    // Do not do this (SQL injection HACK)
                    //command.CommandText = $"SELECT * FROM [USERS] WHERE Id = {id}";

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.spUsers_GetById"; // sql stored procedure
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader dataReader = command.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        user.Id = (int)dataReader["Id"];
                        user.UserName = (string)dataReader["UserName"];
                        user.Password = (string)dataReader["Password"];
                        user.FirstName = (string)dataReader["FirstName"];
                        user.LastName = (string)dataReader["LastName"];
                    }
                    dataReader.Close();
                }
                if (user.Id == 0)
                {
                    return null;
                }
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM [NOTES] Where userId = @userId";
                    command.Parameters.AddWithValue("@userId", user.Id);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = (int)dataReader["Id"],
                            Text = (string)dataReader["Text"],
                            Color = (string)dataReader["Color"],
                            Tag = (int)dataReader["Tag"],
                            UserId = (int)dataReader["UserId"]
                        };
                        user.Notes.Add(note);
                    }
                    dataReader.Close();
                }
                return user;
            }
        }

        public void Remove(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM [Users] WHERE Id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(User entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "UPDATE [Users] " +
                        "SET UserName = @userName, Password = @pass, FirstName = @firstName, LastName = @lastName " +
                        "WHERE Id = @id";
                    command.Parameters.AddWithValue("@userName", entity.UserName);
                    command.Parameters.AddWithValue("@pass", entity.Password);
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
