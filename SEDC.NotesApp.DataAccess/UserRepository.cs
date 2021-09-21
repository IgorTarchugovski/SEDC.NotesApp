using SEDC.NotesApp.Models;
using SEDC.NotesApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly NotesAppDbContext _dbContext;

        public UserRepository(NotesAppDbContext context)
        {
            _dbContext = context;
        }
        public void Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.SingleOrDefault(user => user.Id == id);
        }

        public void Remove(int id)
        {
            User user = _dbContext.Users.SingleOrDefault(user => user.Id == id);
            if (user == null)
            {
                throw new Exception("No such user");
            }
            _dbContext.Users.Remove(user);
        }

        public void Update(User entity)
        {
            User user = _dbContext.Users.SingleOrDefault(user => user.Id == entity.Id);
            if (user == null)
            {
                throw new Exception("No such user");
            }
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            _dbContext.SaveChanges();
        }
    }
}
