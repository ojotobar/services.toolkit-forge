# EFCore.DataForge

[![NuGet](https://img.shields.io/nuget/v/EFCore.DataForge.svg)](https://www.nuget.org/packages/EFCore.DataForge)
[![NuGet Downloads](https://img.shields.io/nuget/dt/EFCore.DataForge.svg)](https://www.nuget.org/packages/EFCore.DataForge)

# Services.Toolkit.Forge

**Services.Toolkit.Forge** is a reusable .NET toolkit providing cross-cutting features for your services, including:

- **Global error handling middleware** to capture and format unhandled exceptions.
- **Centralized logger manager** for consistent logging across multiple services.
- **Standardized error response models** for consistent API output.

This library is designed for microservices, distributed systems, or any project that needs consistent error handling and logging.

---

## 📦 Installation

Install from NuGet:

```powershell
dotnet add package Services.Toolkit.Forge
```

---

## 🚀 Quick Start

### 1. Register the Logger Manager
In `Program.cs` or `Startup.ConfigureServices`:

```csharp
builder.Services.AddLoggerManager();
```

### 2. Use the Global Error Handler
In `Program.cs` or `Startup.Configure`:

```csharp
app.UseUnifiedErrorHandler();
```

---

## 🛠 Example

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add Logger Manager
builder.Services.AddLoggerManager();

var app = builder.Build();

// Use Global Error Handler Middleware
app.UseUnifiedErrorHandler();

app.MapGet("/", () =>
{
    throw new Exception("Test unhandled exception");
});

app.Run();
```

If an unhandled exception occurs, the middleware logs the error and returns a standardized JSON response like:

```json
{
  "statusCode": 500,
  "message": "Internal Server Error"
}
```

---

## 📂 Folder Structure

```
Middleware/
  UnifiedErrorHandlerMiddleware.cs
  MiddlewareExtensions.cs

Logging/
  LoggerManager.cs
  LoggerExtensions.cs
```
---
## ⚖ License

This project is licensed under the MIT License - see the LICENSE file for details.