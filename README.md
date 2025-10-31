# equipment-tracker-desktop-fixed

A desktop application for tracking equipment, built with .NET Framework and SQLite.

## Prerequisites

- **Visual Studio 2019 or later** with .NET Framework development workload
- **.NET Framework 4.7.2 or higher**
- **SQLite** for local database storage

## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed.git
cd equipment-tracker-desktop-fixed
```

### 2. Install Required NuGet Packages
Open the solution in Visual Studio and restore NuGet packages:
- **System.Data.SQLite** - SQLite database provider for .NET
- **System.Data.SQLite.Core** - Core SQLite functionality
- **System.Data.SQLite.EF6** - Entity Framework 6 support (if using EF)

```bash
Install-Package System.Data.SQLite
Install-Package System.Data.SQLite.Core
```

### 3. Database Configuration
The application uses SQLite for local data storage. The database file will be created automatically on first run.

- Database file location: `equipment-tracker.db` (in application directory)
- Connection string is configured in `App.config` or `appsettings.json`

### 4. Build and Run
1. Open the solution file (`.sln`) in Visual Studio
2. Build the solution: `Ctrl + Shift + B`
3. Run the application: `F5`

## Project Structure

```
equipment-tracker-desktop-fixed/
├── App.config              # Application configuration
├── Database/               # SQLite database files
├── Models/                 # Data models
├── Views/                  # UI forms/windows
├── Controllers/            # Business logic
└── Resources/              # Images, icons, etc.
```

## Technologies Used

- **.NET Framework 4.7.2+** - Application framework
- **Windows Forms / WPF** - Desktop UI
- **SQLite** - Embedded database
- **System.Data.SQLite** - ADO.NET provider for SQLite

## Development Notes

- Target Platform: Windows Desktop
- Architecture: x86 or x64
- Database: SQLite (file-based, no server required)
- UI Framework: Windows Forms or WPF

## License

This project is open source and available under the MIT License.
