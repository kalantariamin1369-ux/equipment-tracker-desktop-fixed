# Equipment Tracker - EXE Installer Build Script
# This script creates both MSI and portable ZIP packages

param(
    [string]$Configuration = "Release",
    [string]$Platform = "Any CPU",
    [switch]$SkipBuild,
    [switch]$Verbose
)

# Enable verbose output if requested
if ($Verbose) {
    $VerbosePreference = "Continue"
}

Write-Host "=== Equipment Tracker Installer Build Script ===" -ForegroundColor Green
Write-Host "Configuration: $Configuration"
Write-Host "Platform: $Platform"
Write-Host "Skip Build: $SkipBuild"
Write-Host ""

# Function to check if a command exists
function Test-Command($command) {
    try {
        Get-Command $command -ErrorAction Stop
        return $true
    } catch {
        return $false
    }
}

# Function to download and setup WiX
function Setup-WiX {
    $wixPath = "$env:TEMP\wix"
    $wixUrl = "https://github.com/wixtoolset/wix3/releases/download/wix3112rtm/wix311-binaries.zip"
    $wixZip = "$env:TEMP\wix311.zip"
    
    Write-Host "Setting up WiX Toolset..." -ForegroundColor Yellow
    
    if (!(Test-Path $wixPath)) {
        Write-Verbose "Downloading WiX from $wixUrl"
        try {
            Invoke-WebRequest -Uri $wixUrl -OutFile $wixZip -UseBasicParsing
            Expand-Archive -Path $wixZip -DestinationPath $wixPath -Force
            Remove-Item $wixZip -Force
        } catch {
            Write-Error "Failed to download or extract WiX: $($_.Exception.Message)"
            exit 1
        }
    }
    
    # Add WiX to PATH for this session
    $env:PATH = "$wixPath;" + $env:PATH
    
    # Verify WiX installation
    if (!(Test-Command "candle.exe")) {
        Write-Error "WiX setup failed - candle.exe not found in PATH"
        exit 1
    }
    
    Write-Host "✓ WiX Toolset ready" -ForegroundColor Green
}

# Function to build the solution
function Build-Solution {
    Write-Host "Building Equipment Tracker solution..." -ForegroundColor Yellow
    
    # Check for MSBuild
    if (!(Test-Command "msbuild.exe")) {
        Write-Error "MSBuild not found. Please run this script from a Visual Studio Developer Command Prompt or install Build Tools for Visual Studio."
        exit 1
    }
    
    # Check for NuGet
    if (!(Test-Command "nuget.exe")) {
        Write-Host "NuGet not found in PATH. Attempting to use local NuGet..." -ForegroundColor Yellow
        
        # Try to find NuGet in common locations
        $nugetPaths = @(
            "$env:LOCALAPPDATA\Microsoft\VisualStudio\*\Extensions\*\NuGet.exe",
            "$env:ProgramFiles(x86)\NuGet\nuget.exe",
            "$env:ProgramFiles\NuGet\nuget.exe"
        )
        
        $nugetFound = $false
        foreach ($path in $nugetPaths) {
            $nugetExe = Get-ChildItem $path -ErrorAction SilentlyContinue | Select-Object -First 1
            if ($nugetExe) {
                $env:PATH = "$(Split-Path $nugetExe.FullName);" + $env:PATH
                $nugetFound = $true
                break
            }
        }
        
        if (!$nugetFound) {
            Write-Warning "NuGet not found. Package restore may fail."
        }
    }
    
    try {
        # Restore NuGet packages
        Write-Verbose "Restoring NuGet packages..."
        & nuget restore EquipmentTracker.sln
        
        if ($LASTEXITCODE -ne 0) {
            Write-Warning "NuGet restore returned exit code $LASTEXITCODE"
        }
        
        # Build solution
        Write-Verbose "Building solution..."
        & msbuild EquipmentTracker.sln /p:Configuration=$Configuration /p:Platform="$Platform" /p:TargetFrameworkVersion=v4.6 /verbosity:minimal
        
        if ($LASTEXITCODE -ne 0) {
            Write-Error "Build failed with exit code $LASTEXITCODE"
            exit 1
        }
        
        # Verify build output
        $buildOutput = "bin\$Configuration\EquipmentTracker.exe"
        if (!(Test-Path $buildOutput)) {
            Write-Error "Build output not found at $buildOutput"
            exit 1
        }
        
        Write-Host "✓ Build completed successfully" -ForegroundColor Green
        
    } catch {
        Write-Error "Build failed: $($_.Exception.Message)"
        exit 1
    }
}

# Function to create MSI installer
function Build-MSIInstaller {
    Write-Host "Creating MSI installer..." -ForegroundColor Yellow
    
    $sourceDir = Resolve-Path "bin\$Configuration"
    $installerDir = "installer"
    
    # Ensure installer directory exists
    if (!(Test-Path $installerDir)) {
        Write-Error "Installer directory not found. Please ensure installer\setup.wxs exists."
        exit 1
    }
    
    # Create output directory
    $outputDir = "$installerDir\output"
    New-Item -ItemType Directory -Force -Path $outputDir | Out-Null
    
    # Copy build artifacts
    Write-Verbose "Copying build artifacts to installer directory..."
    Copy-Item -Path "$sourceDir\*" -Destination $outputDir -Recurse -Force
    
    try {
        # Set environment variable for WiX
        $env:SourceDir = $outputDir
        
        # Compile WiX source
        Write-Verbose "Compiling WiX source files..."
        & candle.exe -ext WixUIExtension -ext WixUtilExtension -dSourceDir="$outputDir" "$installerDir\setup.wxs" -o "$installerDir\setup.wixobj"
        
        if ($LASTEXITCODE -ne 0) {
            Write-Error "WiX compilation failed with exit code $LASTEXITCODE"
            exit 1
        }
        
        # Link MSI
        Write-Verbose "Linking MSI installer..."
        & light.exe -ext WixUIExtension -ext WixUtilExtension "$installerDir\setup.wixobj" -o "$installerDir\EquipmentTracker-Setup.msi"
        
        if ($LASTEXITCODE -ne 0) {
            Write-Error "WiX linking failed with exit code $LASTEXITCODE"
            exit 1
        }
        
        # Clean up intermediate files
        Remove-Item "$installerDir\setup.wixobj" -ErrorAction SilentlyContinue
        
        Write-Host "✓ MSI installer created: installer\EquipmentTracker-Setup.msi" -ForegroundColor Green
        
    } catch {
        Write-Error "MSI creation failed: $($_.Exception.Message)"
        exit 1
    }
}

# Function to create portable ZIP
function Build-PortableZIP {
    Write-Host "Creating portable ZIP package..." -ForegroundColor Yellow
    
    $sourceDir = "bin\$Configuration"
    $portableDir = "installer\portable"
    $zipFile = "installer\EquipmentTracker-Portable.zip"
    
    try {
        # Create portable directory
        New-Item -ItemType Directory -Force -Path $portableDir | Out-Null
        
        # Copy application files
        Copy-Item -Path "$sourceDir\*" -Destination $portableDir -Recurse -Force
        
        # Create README for portable version
        $portableReadme = @"
Equipment Tracker - Portable Version
===================================

This is a portable version of Equipment Tracker that requires no installation.

System Requirements:
- Windows 7, 8, 8.1, 10, or 11
- .NET Framework 4.6 or higher

To run:
1. Extract all files to a folder
2. Double-click EquipmentTracker.exe
3. The SQLite database file will be created automatically in the same folder

For support and updates, visit:
https://github.com/kalantariamin1369-ux/equipment-tracker-desktop-fixed

Version: 1.0.12
Build Date: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
"@
        
        Set-Content -Path "$portableDir\README.txt" -Value $portableReadme -Encoding UTF8
        
        # Remove existing ZIP if it exists
        if (Test-Path $zipFile) {
            Remove-Item $zipFile -Force
        }
        
        # Create ZIP archive
        Compress-Archive -Path "$portableDir\*" -DestinationPath $zipFile -CompressionLevel Optimal
        
        # Clean up portable directory
        Remove-Item $portableDir -Recurse -Force
        
        Write-Host "✓ Portable ZIP created: $zipFile" -ForegroundColor Green
        
    } catch {
        Write-Error "Portable ZIP creation failed: $($_.Exception.Message)"
        exit 1
    }
}

# Function to display results
function Show-Results {
    Write-Host ""
    Write-Host "=== Build Results ===" -ForegroundColor Green
    
    $msiFile = "installer\EquipmentTracker-Setup.msi"
    $zipFile = "installer\EquipmentTracker-Portable.zip"
    
    if (Test-Path $msiFile) {
        $msiSize = [math]::Round((Get-Item $msiFile).Length / 1MB, 2)
        Write-Host "✓ MSI Installer: $msiFile ($msiSize MB)" -ForegroundColor Green
    }
    
    if (Test-Path $zipFile) {
        $zipSize = [math]::Round((Get-Item $zipFile).Length / 1MB, 2)
        Write-Host "✓ Portable ZIP: $zipFile ($zipSize MB)" -ForegroundColor Green
    }
    
    Write-Host ""
    Write-Host "Installation packages are ready for distribution!" -ForegroundColor Yellow
}

# Main execution
try {
    # Change to script directory
    $scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
    if ($scriptDir) {
        Set-Location $scriptDir
        Set-Location ".."
    }
    
    # Verify we're in the right directory
    if (!(Test-Path "EquipmentTracker.sln")) {
        Write-Error "EquipmentTracker.sln not found. Please run this script from the project root directory."
        exit 1
    }
    
    # Setup WiX
    Setup-WiX
    
    # Build solution (unless skipped)
    if (!$SkipBuild) {
        Build-Solution
    } else {
        Write-Host "Skipping build as requested" -ForegroundColor Yellow
        
        # Verify build output exists
        if (!(Test-Path "bin\$Configuration\EquipmentTracker.exe")) {
            Write-Error "Build output not found. Cannot skip build when no existing build exists."
            exit 1
        }
    }
    
    # Create installers
    Build-MSIInstaller
    Build-PortableZIP
    
    # Show results
    Show-Results
    
} catch {
    Write-Error "Script execution failed: $($_.Exception.Message)"
    exit 1
}