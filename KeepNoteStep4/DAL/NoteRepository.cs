using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class NoteRepository : INoteRepository
    {
        private readonly KeepDbContext _dbContext;

        public NoteRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Note CreateNote(Note note)
        {
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
            return note;
        }

        public bool DeleteNote(int noteId)
        {
            var note = _dbContext.Notes.Find(noteId);
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Note> GetAllNotesByUserId(int userId)
        {
            return _dbContext.Notes.Where(n => n.CreatedBy == userId).ToList();
            
        }

        public Note GetNoteByNoteId(int noteId)
        {
            return _dbContext.Notes.Find(noteId);
        }

        public bool UpdateNote(Note note)
        {
            var existingNote = _dbContext.Notes.Find(note.NoteId);
            if (existingNote != null)
            {
                // Update properties of existingNote
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
