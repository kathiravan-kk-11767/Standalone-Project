# Smart Notification Manager

A .NET 9 WinUI3 application for managing notifications with CI/CD support.

## Project Structure

This solution contains the following projects:

### Main Application
- **SmartNotificationManager** - The startup WinUI3 application (net9.0-windows10.0.19041.0)
  - Entry point for the application
  - References all other projects
  - Configured for WinUI3 when built on Windows

### View Layer
- **SmartNotificationManager.View** - WinUI3 view components (net9.0-windows10.0.19041.0)
  - Contains UI views and components
  - Uses WinUI3 framework

### Business Logic
- **SmartNotificationManager.Entities** - Entity models (.NET 9)
  - Contains data models and entities
  - Notification entity definitions
  
- **SmartNotificationLibrary** - Business logic library (.NET 9)
  - Core notification service implementations
  - Business logic for notification management

### UI Components
- **WinUI3Component** - Reusable WinUI3 components (net9.0-windows10.0.19041.0)
  - Shared UI components
  - Can be used across multiple WinUI3 applications

### Infrastructure/Dependencies
- **WinCommon** - Common Windows utilities (.NET 9)
  - Common utility functions
  - Cross-project helper methods
  
- **WinLogger** - Logging library (.NET 9)
  - Application logging functionality
  - Error tracking and diagnostics
  
- **WinSQLiteDBAdapter** - SQLite database adapter (.NET 9)
  - Database access layer
  - SQLite database operations

## Technology Stack

- **.NET 9** - Latest .NET framework
- **WinUI 3** - Modern Windows UI framework
- **C#** - Primary programming language
- **Windows SDK 10.0.19041.0** - Target Windows 10 SDK

## Building the Solution

### Prerequisites
- .NET 9 SDK
- Visual Studio 2022 (for Windows builds with full WinUI3 support)
- Windows 10 SDK 10.0.19041.0 or higher

### Build Commands

#### On Windows (Full WinUI3 support)
```bash
dotnet restore SmartNotificationManager.sln
dotnet build SmartNotificationManager.sln --configuration Release
```

#### On Linux/macOS (CI/CD mode)
```bash
dotnet restore SmartNotificationManager.sln
dotnet build SmartNotificationManager.sln --configuration Release
```

Note: The solution is configured to build on non-Windows platforms for CI/CD purposes with `EnableWindowsTargeting=true`. Full WinUI3 features are available only when built on Windows.

### Running the Application
```bash
dotnet run --project SmartNotificationManager/SmartNotificationManager.csproj
```

## CI/CD

### Continuous Integration (CI)
The CI workflow runs on every push and pull request:
- Builds the solution on both Ubuntu and Windows
- Runs in Debug and Release configurations
- Executes tests (when available)
- Uploads build artifacts

**Workflow file:** `.github/workflows/ci.yml`

### Continuous Deployment (CD)
The CD workflow runs on version tags or manual trigger:
- Builds the Release configuration on Windows
- Publishes the application
- Creates a deployment package
- Creates a GitHub Release (for tagged versions)

**Workflow file:** `.github/workflows/cd.yml`

### Triggering Deployments

#### Automatic (on tag):
```bash
git tag v1.0.0
git push origin v1.0.0
```

#### Manual:
Go to Actions → CD - Deploy → Run workflow

## Development Guidelines

1. **Target Framework**: All projects target .NET 9
2. **WinUI3 Projects**: SmartNotificationManager, SmartNotificationManager.View, and WinUI3Component use WinUI3
3. **Code Style**: Follow standard C# coding conventions
4. **Dependencies**: Minimize external dependencies; prefer .NET built-in libraries

## Project Dependencies

```
SmartNotificationManager (Startup)
├── SmartNotificationManager.View
│   ├── SmartNotificationManager.Entities
│   └── WinUI3Component
├── SmartNotificationManager.Entities
├── SmartNotificationLibrary
├── WinUI3Component
├── WinCommon
├── WinLogger
└── WinSQLiteDBAdapter
```

## Configuration

The solution uses `Directory.Build.props` for common MSBuild properties:
- Windows SDK versions
- Cross-platform build support (`EnableWindowsTargeting`)

## Contributing

1. Create a feature branch
2. Make your changes
3. Ensure the solution builds successfully
4. Create a pull request

## License

[Specify your license here]

## Contact

[Specify contact information here]
