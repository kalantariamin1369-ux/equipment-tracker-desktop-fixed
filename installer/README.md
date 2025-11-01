# Equipment Tracker - Installer Configuration

This directory contains the configuration files and scripts needed to create installation packages for the Equipment Tracker desktop application.

## Files Overview

### Core Installer Files
- **`setup.wxs`** - WiX Toolset configuration file that defines the MSI installer structure
- **`license.rtf`** - MIT license in RTF format displayed during installation
- **`README.md`** - This documentation file

### Generated Files (during build)
- **`output/`** - Directory containing compiled application files
- **`portable/`** - Temporary directory for portable version creation
- **`EquipmentTracker-Setup.msi`** - Windows MSI installer package
- **`EquipmentTracker-Portable.zip`** - Portable ZIP package

## Installation Package Types

### 1. MSI Installer (`EquipmentTracker-Setup.msi`)
**Professional Windows installer with the following features:**
- ✅ Standard Windows installation experience
- ✅ Add/Remove Programs integration
- ✅ Start Menu shortcuts
- ✅ Desktop shortcut (optional)
- ✅ Registry entries for proper uninstallation
- ✅ Administrative installation support
- ✅ License agreement display
- ✅ Custom installation directory selection

**System Requirements:**
- Windows 7, 8, 8.1, 10, or 11
- .NET Framework 4.6 or higher
- Administrative privileges for installation
- 50 MB available disk space

### 2. Portable ZIP (`EquipmentTracker-Portable.zip`)
**Standalone application package with these benefits:**
- ✅ No installation required
- ✅ Run from any location (USB drive, network share, etc.)
- ✅ No registry modifications
- ✅ No administrative privileges needed
- ✅ Database created in application directory
- ✅ Complete application isolation

## Building Installers

### Method 1: GitHub Actions (Automatic)
The repository is configured with GitHub Actions that automatically build both installer types:

1. **Trigger**: Push to `main` branch or create a release tag
2. **Artifacts**: Both MSI and ZIP files are uploaded as build artifacts
3. **Releases**: Tagged releases automatically attach installer files

### Method 2: Manual Build (Local Development)

#### Prerequisites
- Visual Studio 2017 or later with .NET Framework 4.6 SDK
- PowerShell 5.1 or later
- Internet connection (for WiX Toolset download)

#### Using the PowerShell Script
```powershell
# Navigate to project root
cd path\to\equipment-tracker-desktop-fixed

# Run the build script
.\scripts\build-installer.ps1

# Optional parameters:
.\scripts\build-installer.ps1 -Configuration Release -Verbose
.\scripts\build-installer.ps1 -SkipBuild  # Use existing build output
```

#### Manual WiX Build Process
```batch
# 1. Build the application
msbuild EquipmentTracker.sln /p:Configuration=Release

# 2. Setup WiX Toolset (download from https://wixtoolset.org/)

# 3. Prepare installer files
mkdir installer\output
copy bin\Release\* installer\output\

# 4. Compile WiX source
candle.exe -ext WixUIExtension -dSourceDir=installer\output installer\setup.wxs -o installer\setup.wixobj

# 5. Link MSI installer
light.exe -ext WixUIExtension installer\setup.wixobj -o installer\EquipmentTracker-Setup.msi
```

## WiX Configuration Details

### Product Information
- **Product Name**: Equipment Tracker
- **Manufacturer**: Amin Kalantari
- **Version**: 1.0.12.0
- **Upgrade Code**: `{A1B2C3D4-E5F6-4A5B-8C7D-9E0F1A2B3C4D}`

### Installation Features
- **Program Files**: Main application and dependencies
- **Start Menu**: Application shortcuts in dedicated folder
- **Desktop**: Optional desktop shortcut
- **Registry**: Installation tracking and uninstall information

### Included Components
- `EquipmentTracker.exe` - Main application executable
- `EquipmentTracker.exe.config` - Application configuration
- `System.Data.SQLite.dll` - SQLite database engine
- `SQLite.Interop.dll` - SQLite native library
- Required .NET Framework assemblies

### Installation Directory Structure
```
C:\Program Files\Equipment Tracker\
├── EquipmentTracker.exe
├── EquipmentTracker.exe.config
├── System.Data.SQLite.dll
├── SQLite.Interop.dll
└── [Additional .NET assemblies]
```

## Customization Options

### Modifying the Installer
To customize the installer, edit `setup.wxs`:

1. **Change Product Information**:
   ```xml
   <Product Id="*" Name="Your App Name" Manufacturer="Your Name" Version="1.0.0.0">
   ```

2. **Add/Remove Files**:
   ```xml
   <Component Id="YourComponent" Guid="{NEW-GUID-HERE}">
     <File Id="YourFile" Source="$(var.SourceDir)\YourFile.dll" />
   </Component>
   ```

3. **Modify Shortcuts**:
   ```xml
   <Shortcut Id="YourShortcut" Name="Your App" Target="[#YourExecutable]" />
   ```

### License Agreement
To update the license agreement, modify `license.rtf` using any RTF-compatible editor (WordPad, Word, etc.).

## Troubleshooting

### Common Issues

1. **"candle.exe not found"**
   - Ensure WiX Toolset is installed and in PATH
   - Use the PowerShell script which automatically downloads WiX

2. **"Build failed"**
   - Verify .NET Framework 4.6 SDK is installed
   - Check that all NuGet packages are restored
   - Ensure MSBuild is available (Visual Studio or Build Tools)

3. **"Missing assemblies in installer"**
   - Check the build output in `bin\Release`
   - Verify all required DLLs are present
   - Update `setup.wxs` to include missing components

4. **"Installation fails"**
   - Run installer as Administrator
   - Check Windows Event Log for detailed error messages
   - Verify .NET Framework 4.6+ is installed on target system

### Getting Help

For technical support:
1. Check the [Issues](https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed/issues) page
2. Review the build logs from GitHub Actions
3. Enable verbose logging: `msiexec /i EquipmentTracker-Setup.msi /l*v install.log`

## Version History

### v1.0.12 (Current)
- Initial WiX installer configuration
- Support for both MSI and portable distributions
- Automated GitHub Actions build pipeline
- MIT license integration

---

**Note**: This installer configuration creates production-ready installation packages suitable for enterprise deployment and end-user distribution.