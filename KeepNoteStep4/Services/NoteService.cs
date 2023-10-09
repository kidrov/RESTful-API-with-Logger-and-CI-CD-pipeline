using System.Collections.Generic;
using DAL;
using Entities;
using System.Linq;
using Exceptions;
using System;

namespace Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReminderRepository _reminderRepository;

        public NoteService(INoteRepository noteRepository, ICategoryRepository categoryRepository, IReminderRepository reminderRepository)
        {
            _noteRepository = noteRepository;
            _categoryRepository = categoryRepository;
            _reminderRepository = reminderRepository;
        }

        public Note CreateNote(Note note)
        {
            // Business logic and validation (if needed)
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note cannot be null.");
            }

            // Call the repository to create the note
            return _noteRepository.CreateNote(note);
        }

        public bool DeleteNote(int noteId)
        {
            // Check if the note exists
            var existingNote = _noteRepository.GetNoteByNoteId(noteId);
            if (existingNote == null)
            {
                throw new NoteNotFoundException($"Note with ID {noteId} not found.");
            }

            // Call the repository to delete the note
            return _noteRepository.DeleteNote(noteId);
        }

        public List<Note> GetAllNotesByUserId(int userId)
        {
            // Call the repository to get notes by user ID

            return _noteRepository.GetAllNotesByUserId(userId);
        }

        public Note GetNoteByNoteId(int noteId)
        {
            // Call the repository to get the note by ID
            return _noteRepository.GetNoteByNoteId(noteId);
        }

        public bool UpdateNote(int noteId, Note note)
        {
            // Check if the note exists
            var existingNote = _noteRepository.GetNoteByNoteId(noteId);
            if (existingNote == null)
            {
                throw new NoteNotFoundException($"Note with ID {noteId} not found.");
            }

            // Update note properties
            // Add business logic if needed

            return _noteRepository.UpdateNote(existingNote);
        }
    }
}
