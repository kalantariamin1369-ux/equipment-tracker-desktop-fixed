using System;

namespace EquipmentTracker.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Action { get; set; } // Created, Updated, Deleted, Assigned, Returned, Login, Logout, Export, Backup
        public string EntityType { get; set; } // Equipment, User, etc.
        public int EntityId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string PerformedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; }
        public string Details { get; set; }
        public string SessionId { get; set; }
        
        public AuditLog()
        {
            Timestamp = DateTime.Now;
        }
    }
}
