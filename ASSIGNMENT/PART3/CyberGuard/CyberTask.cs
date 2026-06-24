using System;

namespace CyberGuard
{
    public class CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ReminderDisplay => ReminderDate.HasValue ? ReminderDate.Value.ToString("dd MMM yyyy") : "No reminder set";
        public string StatusDisplay => IsCompleted ? "✅ Completed" : "⏳ Pending";

        public override string ToString() => $"[{Id}] {Title} — {StatusDisplay} | Reminder: {ReminderDisplay}";
    }
}