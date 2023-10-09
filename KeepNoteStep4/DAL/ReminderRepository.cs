using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace DAL
{
    // Repository class is used to implement all Data access operations
    public class ReminderRepository : IReminderRepository
    {
        private readonly KeepDbContext _dbContext;

        public ReminderRepository(KeepDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            _dbContext.Reminders.Add(reminder);
            _dbContext.SaveChanges();
            return reminder;
        }

        public bool DeletReminder(int reminderId)
        {
            var reminder = _dbContext.Reminders.Find(reminderId);
            if (reminder != null)
            {
                _dbContext.Reminders.Remove(reminder);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Reminder> GetAllRemindersByUserId(int userId)
        {
            return _dbContext.Reminders.Where(r => r.CreatedBy == userId).ToList();
        }

        public Reminder GetReminderById(int reminderId)
        {
            return _dbContext.Reminders.Find(reminderId);
        }

        public bool UpdateReminder(Reminder reminder)
        {
            var existingReminder = _dbContext.Reminders.Find(reminder.ReminderId);
            if (existingReminder != null)
            {
                // Update properties of existingReminder
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
