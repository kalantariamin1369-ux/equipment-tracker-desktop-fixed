using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using EquipmentTracker.Models;
using Newtonsoft.Json;

namespace EquipmentTracker.Database
{
    public class DatabaseManager
    {
        private readonly string connectionString;
        private readonly string backupPath;
        
        public DatabaseManager(string dbPath = "equipmenttracker.db")
        {
            connectionString = $"Data Source={dbPath};Version=3;";
            backupPath = "Backups";
            InitializeDatabase();
        }
        
        private void InitializeDatabase()
        {
            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);
            
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                CreateTables(conn);
            }
        }
        
        private void CreateTables(SQLiteConnection conn)
        {
            var commands = new[]
            {
                @"CREATE TABLE IF NOT EXISTS Equipment (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    SerialNumber TEXT,
                    AssetTag TEXT UNIQUE,
                    Category TEXT,
                    Manufacturer TEXT,
                    Model TEXT,
                    PurchaseDate TEXT,
                    PurchasePrice REAL,
                    Location TEXT,
                    Status TEXT,
                    Condition TEXT,
                    AssignedTo TEXT,
                    AssignedDate TEXT,
                    ReturnDate TEXT,
                    Barcode TEXT,
                    QRCode TEXT,
                    Notes TEXT,
                    CreatedDate TEXT,
                    ModifiedDate TEXT,
                    CreatedBy TEXT,
                    ModifiedBy TEXT,
                    WarrantyExpiry TEXT,
                    Department TEXT,
                    IsDeleted INTEGER DEFAULT 0,
                    ImagePath TEXT
                )",
                @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT UNIQUE NOT NULL,
                    PasswordHash TEXT NOT NULL,
                    Salt TEXT NOT NULL,
                    Email TEXT,
                    FullName TEXT,
                    Role TEXT,
                    IsActive INTEGER DEFAULT 1,
                    CreatedDate TEXT,
                    LastLoginDate TEXT,
                    TwoFactorSecret TEXT,
                    TwoFactorEnabled INTEGER DEFAULT 0,
                    Department TEXT,
                    PhoneNumber TEXT,
                    IsDeleted INTEGER DEFAULT 0,
                    PreferredLanguage TEXT DEFAULT 'en',
                    Theme TEXT DEFAULT 'Light',
                    FailedLoginAttempts INTEGER DEFAULT 0,
                    LockoutEndDate TEXT
                )",
                @"CREATE TABLE IF NOT EXISTS AuditLogs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Action TEXT,
                    EntityType TEXT,
                    EntityId INTEGER,
                    OldValue TEXT,
                    NewValue TEXT,
                    PerformedBy TEXT,
                    Timestamp TEXT,
                    IPAddress TEXT,
                    Details TEXT,
                    SessionId TEXT
                )",
                @"CREATE INDEX IF NOT EXISTS idx_equipment_status ON Equipment(Status)",
                @"CREATE INDEX IF NOT EXISTS idx_equipment_category ON Equipment(Category)",
                @"CREATE INDEX IF NOT EXISTS idx_audit_timestamp ON AuditLogs(Timestamp)",
                @"CREATE INDEX IF NOT EXISTS idx_users_username ON Users(Username)"
            };
            
            foreach (var cmd in commands)
            {
                using (var command = new SQLiteCommand(cmd, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public void CreateDefaultAdmin()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    var checkCmd = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE Role = 'Admin'", conn);
                    var count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    
                    if (count == 0)
                    {
                        var salt = GenerateSalt();
                        var hash = HashPassword("admin", salt);
                        var insertCmd = new SQLiteCommand(
                            @"INSERT INTO Users (Username, PasswordHash, Salt, Email, FullName, Role, IsActive, CreatedDate, LastLoginDate, PreferredLanguage, Theme) 
                            VALUES (@username, @hash, @salt, @email, @fullname, @role, 1, @created, @lastlogin, 'en', 'Light')", conn);
                        insertCmd.Parameters.AddWithValue("@username", "admin");
                        insertCmd.Parameters.AddWithValue("@hash", hash);
                        insertCmd.Parameters.AddWithValue("@salt", salt);
                        insertCmd.Parameters.AddWithValue("@email", "admin@equipmenttracker.local");
                        insertCmd.Parameters.AddWithValue("@fullname", "System Administrator");
                        insertCmd.Parameters.AddWithValue("@role", "Admin");
                        insertCmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("o"));
                        insertCmd.Parameters.AddWithValue("@lastlogin", DateTime.Now.ToString("o"));
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create default admin: {ex.Message}");
            }
        }
        
        private string GenerateSalt()
        {
            var random = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var salt = new byte[32];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        
        private string HashPassword(string password, string salt)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var saltedPassword = password + salt;
                var bytes = System.Text.Encoding.UTF8.GetBytes(saltedPassword);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        
        public bool BackupDatabase()
        {
            try
            {
                var backupFile = Path.Combine(backupPath, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");
                var sourceFile = connectionString.Replace("Data Source=", "").Replace(";Version=3;", "");
                File.Copy(sourceFile, backupFile, true);
                LogAudit("Backup", "Database", 0, "", backupFile, "System", "Database backup created");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public void LogAudit(string action, string entityType, int entityId, string oldValue, string newValue, string performedBy, string details)
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SQLiteCommand(
                        @"INSERT INTO AuditLogs (Action, EntityType, EntityId, OldValue, NewValue, PerformedBy, Timestamp, Details) 
                        VALUES (@action, @entityType, @entityId, @oldValue, @newValue, @performedBy, @timestamp, @details)", conn);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@entityType", entityType);
                    cmd.Parameters.AddWithValue("@entityId", entityId);
                    cmd.Parameters.AddWithValue("@oldValue", oldValue ?? "");
                    cmd.Parameters.AddWithValue("@newValue", newValue ?? "");
                    cmd.Parameters.AddWithValue("@performedBy", performedBy);
                    cmd.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("o"));
                    cmd.Parameters.AddWithValue("@details", details ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }
    }
}
