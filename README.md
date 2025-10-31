# Equipment Tracker Desktop

[![Build Equipment Tracker Installer](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml/badge.svg)](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/actions/workflows/build-installer.yml)

A minimal desktop application for tracking equipment assets built with C# WinForms and SQLite.

## Features

- **Equipment Management**: Track equipment with essential fields
  - ID
  - Name
  - Type
  - Serial Number
  - Status (Available, In Use, Maintenance, Retired)
  - Purchase Date
  - Price

- **CRUD Operations**: Add, edit, delete, and view equipment
- **Data Grid View**: Display all equipment in an easy-to-read table
- **SQLite Database**: Lightweight local database storage
- **Simple Interface**: Clean and straightforward UI

## Technical Details

- **Framework**: .NET Framework 4.7.2
- **UI**: Windows Forms (WinForms)
- **Database**: SQLite
- **Language**: C#

## Installation

### Option 1: Installer (Recommended)
1. Download the latest `EquipmentTracker-Installer.exe` from the [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases) page
2. Run the installer and follow the prompts
3. Launch the application from the Start Menu

### Option 2: Portable Version
1. Download the latest `EquipmentTracker-Portable.zip` from the [Releases](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/releases) page
2. Extract the ZIP file to your desired location
3. Run `EquipmentTracker.exe`

## Usage

1. **Add Equipment**: Click the "Add" button to open the form and enter equipment details
2. **Edit Equipment**: Select an equipment row and click "Edit" to modify details
3. **Delete Equipment**: Select an equipment row and click "Delete" to remove it
4. **Refresh**: Click "Refresh" to reload the equipment list from the database

## Building from Source

### Prerequisites
- Visual Studio 2017 or later
- .NET Framework 4.7.2 SDK
- System.Data.SQLite NuGet package

### Build Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed.git
   ```
2. Open `EquipmentTracker.sln` in Visual Studio
3. Restore NuGet packages
4. Build the solution (F6)
5. Run the application (F5)

## Project Structure

```
EquipmentTracker/
├── Database/
│   └── DatabaseManager.cs    # SQLite database operations
├── Models/
│   └── Equipment.cs          # Equipment data model
├── Views/
│   ├── MainForm.cs           # Main application window
│   └── EquipmentForm.cs      # Add/Edit equipment dialog
├── Program.cs                # Application entry point
├── App.config                # Application configuration
└── EquipmentTracker.csproj   # Project file
```

## Database Schema

### Equipment Table
| Column | Type | Description |
|--------|------|-------------|
| Id | INTEGER | Primary key (auto-increment) |
| Name | TEXT | Equipment name |
| Type | TEXT | Equipment type/category |
| SerialNumber | TEXT | Serial number (optional) |
| Status | TEXT | Status (Available/In Use/Maintenance/Retired) |
| PurchaseDate | TEXT | Date of purchase (ISO 8601 format) |
| Price | REAL | Purchase price |

## License

MIT License - See LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions, please open an issue on GitHub.
