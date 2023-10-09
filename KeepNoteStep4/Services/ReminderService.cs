using System;
using System.Collections.Generic;
using DAL;
using Entities;
using Exceptions;

namespace Service
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(IReminderRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            // Business logic and validation (if needed)
            if (reminder == null)
            {
                throw new ArgumentNullException(nameof(reminder), "Reminder cannot be null.");
            }

            // Call the repository to create the reminder
            return _reminderRepository.CreateReminder(reminder);
        }

        public bool DeletReminder(int reminderId)
        {
            // Check if the reminder exists
            var existingReminder = _reminderRepository.GetReminderById(reminderId);
            if (existingReminder == null)
            {
                throw new ReminderNotFoundException($"Reminder with ID {reminderId} not found.");
            }

            // Call the repository to delete the reminder
            return _reminderRepository.DeletReminder(reminderId);
        }

        public List<Reminder> GetAllRemindersByUserId(int userId)
        {
            // Call the repository to get reminders by user ID
            return _reminderRepository.GetAllRemindersByUserId(userId);
        }

        public Reminder GetReminderById(int reminderId)
        {
            // Call the repository to get the reminder by ID
            return _reminderRepository.GetReminderById(reminderId);
        }

        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            // Check if the reminder exists
            var existingReminder = _reminderRepository.GetReminderById(reminderId);
            if (existingReminder == null)
            {
                throw new ReminderNotFoundException($"Reminder with ID {reminderId} not found.");
            }

            // Update reminder properties
            // Add business logic if needed

            return _reminderRepository.UpdateReminder(existingReminder);
        }
    }
}
