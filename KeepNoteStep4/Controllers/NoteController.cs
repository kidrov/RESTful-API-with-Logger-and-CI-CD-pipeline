using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Entities;
using log4net;
using System;
using System.Net;

namespace KeepNote.Controllers
{
    [ApiController]
    [Route("api/note")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(NoteController));

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public IActionResult CreateNote(Note note)
        {
            _logger.Info("CreateNote method called.");
            try
            {
                // Call the NoteService to create a note
                var createdNote = _noteService.CreateNote(note);
                return Created("", createdNote); // 201 Created
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in CreateNote: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating the note.");
            }
        }

        [HttpDelete("{noteId}")]
        public IActionResult DeleteNote(int noteId)
        {
            _logger.Info("DeleteNote method called.");
            try
            {
                // Call the NoteService to delete a note
                var result = _noteService.DeleteNote(noteId);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in DeleteNote: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the note.");
            }
        }

        [HttpPut("{noteId}")]
        public IActionResult UpdateNote(int noteId, Note note)
        {
            _logger.Info("UpdateNote method called.");
            try
            {
                // Call the NoteService to update a note
                var result = _noteService.UpdateNote(noteId, note);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in UpdateNote: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the note.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetNotesByUserId(int userId)
        {
            _logger.Info("GetNotesByUserId method called.");
            try
            {
                // Call the NoteService to get notes by user ID
                var notes = _noteService.GetAllNotesByUserId(userId);
                return Ok(notes); // 200 OK
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GetNotesByUserId: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching notes.");
            }
        }
    }
}
