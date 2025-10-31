# Equipment Tracker Desktop

A Windows Forms desktop application for tracking and managing equipment inventory using SQLite database with Microsoft.Data.Sqlite.

## Features

- Add, edit, and delete equipment records
- Search and filter equipment
- Track equipment status (Available, In Use, Maintenance, Retired)
- Store equipment details including name, type, serial number, purchase date, and price
- SQLite database for local data storage using Microsoft.Data.Sqlite

## Prerequisites

- Visual Studio 2017 or later
- .NET Framework 4.7.2 SDK
- Microsoft.Data.Sqlite NuGet package (version 7.0.0)

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
│   └── DatabaseManager.cs    # SQLite database operations using Microsoft.Data.Sqlite
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

## Database Migration

This project has been migrated from System.Data.SQLite to Microsoft.Data.Sqlite (version 7.0.0) for improved compatibility and modern .NET support. The migration includes:

- Replaced System.Data.SQLite NuGet packages with Microsoft.Data.Sqlite
- Updated DatabaseManager.cs to use Microsoft.Data.Sqlite namespaces and types
- All CRUD operations now use SqliteConnection, SqliteCommand, and SqliteDataReader from Microsoft.Data.Sqlite

## License

MIT License - See LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

If you encounter any issues or have questions, please open an issue on GitHub.
