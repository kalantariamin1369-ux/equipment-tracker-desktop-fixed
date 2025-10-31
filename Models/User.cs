using System;

namespace EquipmentTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Admin, Manager, User, Viewer
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string TwoFactorSecret { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string PreferredLanguage { get; set; } // en, fa
        public string Theme { get; set; } // Light, Dark
        public int FailedLoginAttempts { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        
        public User()
        {
            CreatedDate = DateTime.Now;
            LastLoginDate = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
            Role = "User";
            TwoFactorEnabled = false;
            PreferredLanguage = "en";
            Theme = "Light";
            FailedLoginAttempts = 0;
        }
    }
}
