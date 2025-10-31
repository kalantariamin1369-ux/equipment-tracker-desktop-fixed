# 🏢 Equipment Tracker Desktop

[![Build Equipment Tracker Installer](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml/badge.svg)](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml)

A **comprehensive desktop application** for tracking and managing equipment assets with advanced features including authentication, role-based access control, reporting, and cross-Windows compatibility.

## ✨ Key Features

### 🔐 Security & Authentication
- **Secure User Authentication** with password hashing and salt
- **Two-Factor Authentication (2FA)** support using Google Authenticator
- **Role-Based Access Control** (Admin, Manager, User, Viewer)
- **Account Lockout Protection** after failed login attempts
- **Session Management** with configurable timeout

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
- **Backup History** management

### 🔔 Notifications
- **Email Notifications** for important events (configurable SMTP)
- **In-App Notifications** for real-time alerts
- **Customizable Alerts** for warranty expiry, assignments, returns

### 🏷️ Barcode & QR Code
- **QR Code Generation** for each asset
- **Barcode Support** with multiple formats
- **Barcode Scanning** for quick asset lookup
- **Printable Labels** with QR/barcode

### 🎨 User Interface
- **Dark Theme** for low-light environments
- **Light Theme** for standard use
- **Theme Persistence** per user
- **Responsive Layout** with resizable panels

### 🌐 Localization
- **English Language** support
- **Persian (Farsi) Language** support
- **RTL Text Support** for Persian
- **Easy Language Switching**

### 💻 Windows Compatibility
- ✅ **Windows Vista** (SP2)
- ✅ **Windows 7** (SP1)
- ✅ **Windows 8** and **8.1**
- ✅ **Windows 10** (all versions)
- ✅ **Windows 11** (all versions)
- Compatible with both **32-bit and 64-bit** architectures

## 📥 Installation

### Option 1: MSI Installer (Recommended)

1. Go to [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases)
2. Download the latest `EquipmentTracker-Setup.msi`
3. Run the installer with **administrator privileges**
4. Follow the setup wizard
5. Launch **Equipment Tracker** from Start Menu
6. Default login: `admin` / `admin` (change immediately!)

### Option 2: Portable Version

1. Download `EquipmentTracker-Portable.zip` from [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases)
2. Extract to any folder
3. Run `EquipmentTracker.exe`
4. No installation required!

## 🛠️ Development Setup

### Prerequisites
- **Visual Studio 2019 or later** with .NET desktop development workload
- **.NET Framework 4.7.2** or higher
- **NuGet Package Manager**

### Clone & Build

```bash
git clone https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed.git
cd equipment-tracker-desktop-fixed
```

### Restore NuGet Packages

```bash
nuget restore EquipmentTracker.sln
```

Or in Visual Studio: `Tools` → `NuGet Package Manager` → `Restore NuGet Packages`

### Build Solution

```bash
msbuild EquipmentTracker.sln /p:Configuration=Release /p:Platform="Any CPU"
```

Or in Visual Studio: `Build` → `Build Solution` (Ctrl+Shift+B)

### Run Application

Press `F5` in Visual Studio or run `bin\Release\EquipmentTracker.exe`

## 📦 Dependencies

- **System.Data.SQLite.Core** (1.0.118.0) - SQLite database engine
- **QRCoder** (1.4.3) - QR code generation
- **ZXing.Net** (0.16.9) - Barcode scanning
- **iTextSharp** (5.5.13.3) - PDF generation
- **Newtonsoft.Json** (13.0.3) - JSON serialization
- **Google.Authenticator** (2.3.0) - Two-factor authentication
- **System.Windows.Forms.DataVisualization** - Charts and graphs

## 🔧 Configuration

Edit `App.config` to customize:

```xml
<appSettings>
  <add key="DatabasePath" value="equipmenttracker.db" />
  <add key="BackupPath" value="Backups" />
  <add key="EnableAutoBackup" value="true" />
  <add key="BackupIntervalHours" value="24" />
  <add key="DefaultLanguage" value="en" />
  <add key="EnableEmailNotifications" value="false" />
  <add key="SmtpServer" value="" />
  <add key="SmtpPort" value="587" />
  <add key="SmtpUsername" value="" />
  <add key="SmtpPassword" value="" />
  <add key="Enable2FA" value="false" />
  <add key="SessionTimeoutMinutes" value="30" />
</appSettings>
```

## 🚀 CI/CD Workflow

### Automatic Builds

The GitHub Actions workflow automatically builds installers on every push to `main` branch:

1. **Builds** the .NET Framework solution
2. **Creates** MSI installer using WiX Toolset
3. **Generates** portable ZIP package
4. **Uploads** artifacts
5. **Creates** GitHub releases

### Manual Trigger

1. Go to [Actions tab](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions)
2. Click **Build Equipment Tracker Installer**
3. Click **Run workflow** → **Run workflow**
4. Wait for completion (2-3 minutes)
5. Download artifacts from workflow run

### Download Installers

After workflow completes:
- Go to **Actions** → Select workflow run
- Download **EquipmentTracker-Setup-MSI** artifact
- Download **EquipmentTracker-Portable** artifact

## 📁 Project Structure

```
equipment-tracker-desktop-fixed/
├── .github/
│   └── workflows/
│       └── build-installer.yml    # CI/CD workflow
├── Controllers/                    # Business logic controllers
├── Database/
│   └── DatabaseManager.cs         # Database operations
├── Models/
│   ├── Equipment.cs               # Equipment model
│   ├── User.cs                    # User model
│   └── AuditLog.cs                # Audit trail model
├── Properties/
│   └── AssemblyInfo.cs            # Assembly metadata
├── Views/                         # UI forms and windows
├── App.config                     # Application configuration
├── EquipmentTracker.csproj        # Project file
├── EquipmentTracker.sln           # Solution file
├── packages.config                # NuGet packages
├── Program.cs                     # Application entry point
└── README.md                      # This file
```

## 👥 User Roles

### Admin
- Full system access
- User management
- System configuration
- All asset operations
- Backup/restore
- View all audit logs

### Manager
- Asset management (CRUD)
- View reports
- Export data
- Assign/return assets
- View department audit logs

### User
- View assets
- Request assignments
- Update assigned assets
- View personal history

### Viewer
- Read-only access
- View assets and reports
- No modifications allowed

## 🔒 Security Best Practices

1. **Change Default Password** immediately after first login
2. **Enable 2FA** for all admin accounts
3. **Regular Backups** - enable automatic backups
4. **Strong Passwords** - enforce minimum requirements
5. **Audit Logs** - regularly review for suspicious activity
6. **Update Regularly** - keep application up to date

## 🐛 Troubleshooting

### Database Issues
- Delete `equipmenttracker.db` to reset database
- Check file permissions in application directory
- Restore from backup if database is corrupted

### Email Notifications Not Working
- Verify SMTP server settings in `App.config`
- Check firewall/antivirus blocking SMTP port
- Test with Gmail: use App Password, not regular password

### 2FA Not Working
- Ensure device time is synchronized
- Re-scan QR code in Google Authenticator
- Use backup codes if available

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
