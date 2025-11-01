# Complete Fix Guide: Equipment Tracker Desktop Application

This guide provides step-by-step instructions to resolve the main build errors in the Equipment Tracker Desktop application.

## Overview of Issues

The following critical build errors have been identified:
1. **System.Data.SQLite namespace not found**
2. **SQLiteConnection type not found**
3. **Duplicate InitializeComponent method in MainForm**

---

## Issue 1: System.Data.SQLite Namespace Not Found

### Problem
The compiler cannot find the `System.Data.SQLite` namespace, which is required for SQLite database operations.

### Root Cause
The SQLite NuGet package is not installed or not properly referenced in the project.

### Solution Steps

#### Option A: Using NuGet Package Manager (Visual Studio)
1. Right-click on your project in Solution Explorer
2. Select "Manage NuGet Packages..."
3. Go to the "Browse" tab
4. Search for "System.Data.SQLite"
5. Install the package by "System.Data.SQLite (x86/x64)"
6. Accept any license agreements
7. Rebuild the project

#### Option B: Using Package Manager Console
1. Open Package Manager Console in Visual Studio (Tools → NuGet Package Manager → Package Manager Console)
2. Run the following command:
   ```
   Install-Package System.Data.SQLite
   ```
3. If you need Entity Framework support, also run:
   ```
   Install-Package System.Data.SQLite.EF6
   ```

#### Option C: Using .NET CLI (for .NET Core/.NET 5+)
1. Open terminal/command prompt in project directory
2. Run:
   ```bash
   dotnet add package System.Data.SQLite
   ```

### Verification
After installation, your project file should contain a reference like:
```xml
<PackageReference Include="System.Data.SQLite" Version="1.0.118" />
```

---

## Issue 2: SQLiteConnection Type Not Found

### Problem
The `SQLiteConnection` type cannot be resolved, even after installing the SQLite package.

### Root Cause
Missing using statements or incorrect namespace references.

### Solution Steps

1. **Add Required Using Statements**
   Add these lines at the top of files that use SQLite:
   ```csharp
   using System.Data.SQLite;
   using System.Data;
   ```

2. **Verify Assembly References**
   Ensure these assemblies are referenced:
   - System.Data.SQLite.dll
   - System.Data.dll

3. **Check Target Framework**
   Ensure your project targets a compatible .NET Framework version:
   - .NET Framework 4.6.1 or higher
   - .NET Core 3.1 or higher
   - .NET 5.0 or higher

4. **Platform Target Configuration**
   - For **Any CPU**: Use System.Data.SQLite (Any CPU)
   - For **x64**: Use System.Data.SQLite.x64
   - For **x86**: Use System.Data.SQLite.x86

5. **Example Usage Pattern**
   ```csharp
   using System.Data.SQLite;
   
   // Connection string example
   string connectionString = "Data Source=equipment.db;Version=3;";
   
   using (SQLiteConnection connection = new SQLiteConnection(connectionString))
   {
       connection.Open();
       // Your database operations here
   }
   ```

---

## Issue 3: Duplicate InitializeComponent Method in MainForm

### Problem
The compiler reports duplicate `InitializeComponent()` method definitions in the MainForm class.

### Root Cause
This typically occurs when:
- Designer file (.Designer.cs) and code-behind file (.cs) both contain the method
- Partial class declarations are incorrect
- Manual editing of designer-generated code

### Solution Steps

#### Step 1: Locate the Duplicate
1. Open `MainForm.cs`
2. Open `MainForm.Designer.cs`
3. Search for `InitializeComponent` in both files

#### Step 2: Fix the Duplication

**Option A: Remove Manual Declaration (Recommended)**
1. In `MainForm.cs`, remove any manually added `InitializeComponent()` method
2. Ensure the class is declared as `partial`:
   ```csharp
   public partial class MainForm : Form
   {
       public MainForm()
       {
           InitializeComponent(); // This should be the only call
       }
   }
   ```

**Option B: Regenerate Designer File**
1. Backup your current code
2. Delete `MainForm.Designer.cs`
3. Open `MainForm.cs` in the designer
4. Make a small change (like moving a control)
5. Save the form - this will regenerate the Designer file

#### Step 3: Verify Partial Class Structure
Ensure your files follow this pattern:

**MainForm.cs:**
```csharp
using System;
using System.Windows.Forms;

namespace YourNamespace
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        
        // Your custom methods here
    }
}
```

**MainForm.Designer.cs:**
```csharp
namespace YourNamespace
{
    partial class MainForm
    {
        private void InitializeComponent()
        {
            // Designer-generated code
        }
        
        // Component declarations
    }
}
```

---

## Environment Setup and Prerequisites

### Required Software
1. **Visual Studio 2019 or later** (Community, Professional, or Enterprise)
2. **.NET Framework 4.6.1 or higher** OR **.NET Core 3.1+**
3. **SQLite** (will be installed via NuGet)

### System Requirements
- Windows 10 or later
- At least 4GB RAM
- 500MB free disk space

### Project Configuration Checklist

1. **Target Framework**
   ```xml
   <TargetFramework>net472</TargetFramework>
   ```
   or
   ```xml
   <TargetFramework>net6.0-windows</TargetFramework>
   ```

2. **Platform Target**
   - Set to "Any CPU" for maximum compatibility
   - Or match your SQLite package architecture

3. **Required NuGet Packages**
   ```xml
   <PackageReference Include="System.Data.SQLite" Version="1.0.118" />
   ```

---

## Build and Deployment Commands

### Clean and Rebuild
```bash
# Clean solution
dotnet clean

# Restore packages
dotnet restore

# Build solution
dotnet build

# Run application
dotnet run
```

### For Visual Studio
1. **Build Menu → Clean Solution**
2. **Build Menu → Rebuild Solution**
3. **Debug Menu → Start Debugging** (F5)

---

## Troubleshooting Additional Issues

### Issue: "Could not load file or assembly 'System.Data.SQLite'"
**Solution:**
1. Check if SQLite native libraries are present
2. Ensure platform target matches SQLite package
3. Copy SQLite.Interop.dll to output directory if needed

### Issue: Database file not found
**Solution:**
1. Ensure database file is in the correct directory
2. Use absolute path for testing: `Data Source=C:\path\to\equipment.db`
3. Set database file "Copy to Output Directory" to "Copy always"

### Issue: Designer errors after fixing duplicates
**Solution:**
1. Close Visual Studio
2. Delete `bin` and `obj` folders
3. Reopen Visual Studio
4. Rebuild solution

---

## Final Verification Steps

1. **Clean Build Test**
   - Clean solution completely
   - Rebuild from scratch
   - Verify no compilation errors

2. **Runtime Test**
   - Run the application
   - Test database connectivity
   - Verify all forms load correctly

3. **Package Dependencies**
   - Check that all NuGet packages are properly restored
   - Verify version compatibility

---

## Summary

Following this guide should resolve all three major build issues:
- ✅ SQLite namespace and connection issues resolved through proper NuGet package installation
- ✅ Duplicate InitializeComponent method fixed through proper partial class structure
- ✅ Environment properly configured for successful compilation

If you continue to experience issues after following this guide, please check the project's Issues section on GitHub for additional troubleshooting steps or create a new issue with specific error details.

**Last Updated:** November 2024
**Compatibility:** .NET Framework 4.6.1+, .NET Core 3.1+, .NET 5+
