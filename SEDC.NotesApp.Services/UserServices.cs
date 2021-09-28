using SEDC.NotesApp.DtoModels;
using SEDC.NotesApp.Models.DbModels;
using SEDC.NotesApp.Repositories;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Exceptions;
using SEDC.NotesApp.Shared.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepository<User> _userRepo;
        public UserServices(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public void Create(UserDto user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                throw new BadRequestException("Username and Password are required");
            }
            User newUser = user.ToUser();
            _userRepo.Add(newUser);
        }

        public void Delete(int id)
        {
            User user = _userRepo.GetById(id);
            if (user == null)
            {
                throw new UserException(id, "No user found");
            }
            _userRepo.Remove(id);
        }

        public List<UserDto> GetAll()
        {
            return _userRepo.GetAll().Select(user => user.ToUserDto()).ToList();
        }

        public UserDto GetById(int id)
        {
            User user = _userRepo.GetById(id);
            if (user == null)
            {
                return null;
            }
            return user.ToUserDto();
        }

        public void Update(UserDto user)
        {
            User userDb = _userRepo.GetById(user.Id);
            if (userDb == null)
            {
                throw new UserException(user.Id, "No user found");
            }
            _userRepo.Update(user.ToUser());
        }
    }
}
