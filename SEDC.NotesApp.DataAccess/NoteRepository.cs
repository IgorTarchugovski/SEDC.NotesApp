using SEDC.NotesApp.Models;
using SEDC.NotesApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Repositories
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly NotesAppDbContext _dbContext;

        public NoteRepository(NotesAppDbContext context)
        {
            _dbContext = context;
        }
        public void Add(Note entity)
        {
            _dbContext.Notes.Add(entity);
            _dbContext.SaveChanges();
        }

        public List<Note> GetAll()
        {
            return _dbContext.Notes.ToList();
        }

        public Note GetById(int id)
        {
            return _dbContext.Notes.SingleOrDefault(note => note.Id == id);
        }

        public void Remove(int id)
        {
            Note note = _dbContext.Notes.SingleOrDefault(note => note.Id == id);
            _dbContext.Notes.Remove(note);
            _dbContext.SaveChanges();
        }

        public void Update(Note entity)
        {
            Note note = _dbContext.Notes.SingleOrDefault(note => note.Id == entity.Id);
            note.Color = entity.Color;
            note.Tag = entity.Tag;
            note.UserId = entity.UserId;
            note.Text = entity.Text;
            _dbContext.SaveChanges();
            
        }
    }
}
