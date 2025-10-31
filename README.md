# 🏢 Equipment Tracker Desktop

[![Build Equipment Tracker Installer](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml/badge.svg)](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml)

A **comprehensive desktop application** for tracking and managing equipment assets with advanced features including reporting and cross-Windows compatibility.

## ✨ Key Features

### 📊 Asset Management
- **Comprehensive Equipment Tracking** with detailed fields
- **Asset Assignment & Return** workflow
- **Status Management** (Available, In Use, Maintenance, Retired)
- **Condition Tracking** (Excellent, Good, Fair, Poor)
- **Department & Location** tracking
- **Warranty Expiry** monitoring
- **Custom Notes** and documentation

### 🔍 Search & Filter
- **Advanced Search** capabilities
- **Multi-Column Filtering** by category, status, location, department
- **Sorting Options** for all fields
- **Quick Search** bar for instant results

### 📈 Reporting & Export
- **Export to CSV** for Excel analysis
- **Export to PDF** for professional reports
- **Detailed Reports** with charts and graphs
- **Custom Report** generation
- **Usage Statistics** and analytics

### 📝 Audit Trail
- **Full History Logging** for all actions
- **Audit Reports** showing who did what and when
- **Change Tracking** with old/new values
- **IP Address Logging** for security
- **Session Tracking** for accountability

### 💾 Backup & Recovery
- **Automatic Database Backup** with configurable intervals
- **Manual Backup** on demand
- **Restore Functionality** from backup files
- **Data Integrity Checks** to prevent corruption
- **Backup History** tracking

### 🖨️ QR Code & Barcode Support
- **QR Code Generation** for equipment tags
- **Barcode Scanning** support
- **Label Printing** integration
- **Mobile-friendly** QR code access

### 📧 Notifications
- **Email Notifications** for important events
- **Assignment Notifications** when equipment is assigned
- **Expiry Alerts** for warranties and maintenance
- **Custom Notification Rules** for different scenarios

### 🎨 User Interface
- **Modern Windows Forms** design
- **Dark/Light Theme** support
- **Responsive Layout** adapting to window size
- **Customizable Dashboard** with widgets
- **Keyboard Shortcuts** for power users
- **Accessibility Features** for better usability

## 🚀 Installation

### Prerequisites
- Windows 7 SP1 or later (Windows 10/11 recommended)
- .NET Framework 4.7.2 or later
- 50 MB free disk space
- Administrator privileges for installation

### Installation Steps

1. **Download** the latest installer from [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases)
2. **Run** `EquipmentTrackerInstaller.msi` as Administrator
3. **Follow** the installation wizard
4. **Launch** Equipment Tracker from Start Menu or Desktop

### Silent Installation
For enterprise deployment:
```bash
msiexec /i EquipmentTrackerInstaller.msi /quiet /norestart
```

## 📦 Dependencies

This application uses the following NuGet packages:

- **System.Data.SQLite.Core** (1.0.118.0) - SQLite database engine
- **QRCoder** (1.4.3) - QR code generation
- **ZXing.Net** (0.16.9) - Barcode scanning
- **iTextSharp** (5.5.13.3) - PDF generation
- **Newtonsoft.Json** (13.0.3) - JSON serialization

Framework assemblies:
- **System.Windows.Forms.DataVisualization** - Charts and graphs

## 🛠️ Configuration

### Database Configuration
The application uses SQLite database (`equipmenttracker.db`) stored in:
```
%APPDATA%\EquipmentTracker\equipmenttracker.db
```

### Email Configuration
Edit `App.config` to configure SMTP settings:
```xml
<appSettings>
  <add key="SmtpServer" value="smtp.gmail.com" />
  <add key="SmtpPort" value="587" />
  <add key="SmtpUsername" value="your-email@gmail.com" />
  <add key="SmtpPassword" value="your-app-password" />
  <add key="SmtpEnableSSL" value="true" />
</appSettings>
```

## 📖 User Guide

### First Time Setup

1. **Launch** the application
2. **Configure Email** for notifications (optional)
3. **Setup Equipment Categories** and Departments
4. **Begin tracking** your equipment

### Adding Equipment

1. Click **Add Equipment** button
2. Fill in required fields:
   - Equipment Name
   - Category
   - Serial Number
   - Purchase Date
   - Location
   - Department
3. Optional fields:
   - Purchase Price
   - Warranty Expiry
   - Condition
   - Notes
4. Click **Save** to add equipment

### Assigning Equipment

1. Select equipment from list
2. Click **Assign** button
3. Choose assignee
4. Set expected return date (optional)
5. Add assignment notes
6. Click **Confirm Assignment**

### Generating Reports

1. Go to **Reports** menu
2. Select report type:
   - Equipment Summary
   - Assignment History
   - Audit Log
   - Custom Report
3. Configure filters and date range
4. Choose export format (PDF/CSV)
5. Click **Generate Report**

## 🔒 Security Best Practices

1. **Regular Backups** - configure automatic backups
2. **Strong Passwords** - enforce minimum requirements (if authentication added)
3. **Audit Logs** - regularly review for suspicious activity
4. **Update Regularly** - keep application up to date
5. **Secure Database** - protect database files with appropriate permissions

## 🐛 Troubleshooting

### Database Issues
- Delete `equipmenttracker.db` to reset database
- Check file permissions in application directory
- Restore from backup if database is corrupted

### Email Notifications Not Working
- Verify SMTP server settings in `App.config`
- Check firewall/antivirus blocking SMTP port
- Test with Gmail: use App Password, not regular password

### Installation Fails
- Run installer as Administrator
- Disable antivirus temporarily
- Check Windows version compatibility
- Ensure .NET Framework 4.7.2 is installed

## 📝 License

This project is licensed under the MIT License.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📧 Support

For issues and questions:
- Open an [Issue](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/issues)
- Check existing documentation
- Review troubleshooting section

## 🔄 Version History

See [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases) for version history and changelogs.

## 🙏 Acknowledgments

- SQLite for reliable embedded database
- WiX Toolset for installer creation
- GitHub Actions for CI/CD automation
- All open-source contributors

---

**Made with ❤️ for Equipment Management**
