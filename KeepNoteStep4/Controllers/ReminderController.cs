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
    [Route("api/reminder")]
    [Authorize]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ReminderController));

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpPost]
        public IActionResult CreateReminder(Reminder reminder)
        {
            _logger.Info("CreateReminder method called.");
            try
            {
                // Call the ReminderService to create a reminder
                var createdReminder = _reminderService.CreateReminder(reminder);
                return Created("", createdReminder); // 201 Created
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in CreateReminder: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while creating the reminder.");
            }
        }

        [HttpDelete("{reminderId}")]
        public IActionResult DeleteReminder(int reminderId)
        {
            _logger.Info("DeleteReminder method called.");
            try
            {
                // Call the ReminderService to delete a reminder
                var result = _reminderService.DeletReminder(reminderId);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in DeleteReminder: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the reminder.");
            }
        }

        [HttpPut("{reminderId}")]
        public IActionResult UpdateReminder(int reminderId, Reminder reminder)
        {
            _logger.Info("UpdateReminder method called.");
            try
            {
                // Call the ReminderService to update a reminder
                var result = _reminderService.UpdateReminder(reminderId, reminder);
                if (result)
                {
                    return Ok(); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in UpdateReminder: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while updating the reminder.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetRemindersByUserId(int userId)
        {
            _logger.Info("GetRemindersByUserId method called.");
            try
            {
                // Call the ReminderService to get reminders by user ID
                var reminders = _reminderService.GetAllRemindersByUserId(userId);
                return Ok(reminders); // 200 OK
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GetRemindersByUserId: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching reminders.");
            }
        }

        [HttpGet("{reminderId}")]
        public IActionResult GetReminderById(int reminderId)
        {
            _logger.Info("GetReminderById method called.");
            try
            {
                // Call the ReminderService to get a reminder by ID
                var reminder = _reminderService.GetReminderById(reminderId);
                if (reminder != null)
                {
                    return Ok(reminder); // 200 OK
                }
                return NotFound(); // 404 Not Found
            }
            catch (Exception ex)
            {
                _logger.Error($"Error in GetReminderById: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred while fetching the reminder.");
            }
        }
    }
}
