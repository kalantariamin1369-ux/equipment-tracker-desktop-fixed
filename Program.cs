using System;
using System.Windows.Forms;
using EquipmentTracker.Database;

namespace EquipmentTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            
            try
            {
                // Initialize database
                var dbManager = new DatabaseManager();
                dbManager.CreateDefaultAdmin();
                
                // Check for automatic backup
                var config = System.Configuration.ConfigurationManager.AppSettings;
                bool enableAutoBackup = bool.Parse(config["EnableAutoBackup"] ?? "true");
                if (enableAutoBackup)
                {
                    // Perform backup on startup
                    dbManager.BackupDatabase();
                }
                
                // Start the application with login form
                // For now, we'll run MainForm directly
                // In production, start with LoginForm first
                Application.Run(new Views.MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
