# MVCProject - Travel & Tourism Booking System

A modern ASP.NET Core MVC web application built with .NET 10, designed for managing travel and tourism bookings with entity framework integration, dependency injection, and a clean architecture structure.

## ?? Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Architecture](#architecture)
- [Available Routes](#available-routes)
- [Services](#services)
- [Dependencies](#dependencies)

## ?? Overview

MVCProject is an ASP.NET Core MVC application designed for managing travel and tourism bookings. It demonstrates:
- Clean architecture patterns with separated concerns
- Entity Framework Core integration with SQL Server for data persistence
- ASP.NET Core Identity for user authentication and authorization
- Object mapping using Mapster for DTOs and domain models
- Responsive UI with Bootstrap for mobile-friendly design
- Travel booking workflow management
- User profile and booking history management

## ?? Features

- **User Authentication** - Secure login and registration for travelers
- **Destination Browsing** - Search and explore travel destinations
- **Booking Management** - Create, view, and manage travel bookings
- **Itinerary Planning** - Organize tours and activities
- **Payment Processing** - Secure booking confirmations
- **User Profiles** - Manage personal travel information and preferences
- **Booking History** - Access past and upcoming reservations
- **Admin Dashboard** - Manage destinations, tours, and user bookings (future implementation)

## ??? Tech Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| **.NET** | 10.0 | Runtime platform |
| **ASP.NET Core MVC** | 10.0 | Web framework |
| **Entity Framework Core** | 10.0.5 | ORM and data access |
| **ASP.NET Core Identity** | 10.0.5 | Authentication & Authorization |
| **Mapster** | 10.0.7 | Object mapping |
| **SQL Server** | 10.0.5 | Database |

## ?? Prerequisites

Before running this project, ensure you have:

- **.NET 10 SDK** or later - [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio 2026** (Community, Professional, or Enterprise) with ASP.NET workload
  - OR **Visual Studio Code** with C# DevKit extension
- **SQL Server** (LocalDB, Express, or full version)
- **PowerShell** or **Command Prompt** for terminal commands

## ?? Project Structure

\\\
MVCProject/
+- AppConfigurations/          # Application configuration classes
+- Controllers/                # MVC Controllers
¦  +- HomeController.cs        # Home page controller
+- Data/                       # Database context and migrations
+- MappingRegisters/           # Mapster mapping profiles
+- Models/                     # Domain models
¦  +- ErrorViewModel.cs        # Error page model
+- Repositories/               # Data access layer (DAL)
+- Services/                   # Business logic and application services
+- ViewModels/                 # View-specific models (DTOs)
+- Views/                      # Razor views (.cshtml)
¦  +- Home/
¦  ¦  +- Index.cshtml         # Home page
¦  ¦  +- Privacy.cshtml        # Privacy policy page
¦  +- Shared/
¦  ¦  +- Error.cshtml         # Error page template
¦  ¦  +- _Layout.cshtml        # Master layout
¦  ¦  +- _Layout.cshtml.css    # Layout styles
¦  ¦  +- _ValidationScriptsPartial.cshtml
¦  +- _ViewImports.cshtml      # Global imports for views
¦  +- _ViewStart.cshtml        # View startup script
+- wwwroot/                    # Static files (CSS, JS, images)
+- Properties/                 # Project properties and launch settings
+- appsettings.json            # Application settings
+- appsettings.Development.json # Development-specific settings
+- MVCProject.csproj           # Project file
+- Program.cs                  # Application startup and configuration

\\\

### Folder Descriptions

| Folder | Purpose |
|--------|---------|
| \AppConfigurations/\ | Stores application-wide configuration classes |
| \Controllers/\ | Contains MVC controller classes handling HTTP requests |
| \Data/\ | Database context, models, and Entity Framework migrations |
| \MappingRegisters/\ | Mapster configuration for object-to-object mapping |
| \Models/\ | Domain models representing core business entities |
| \Repositories/\ | Data access abstraction layer |
| \Services/\ | Business logic, application services, and domain operations |
| \ViewModels/\ | Data transfer objects for view rendering |
| \Views/\ | Razor markup templates (.cshtml files) |
| \wwwroot/\ | Static web assets (CSS, JavaScript, images) |

## ?? Getting Started

### 1. Clone or Download the Repository

\\\ash
# If cloning from Git
git clone <repository-url>
cd MVCProject
\\\

### 2. Restore NuGet Packages

\\\powershell
dotnet restore
\\\

### 3. Update Database (if using EF Core migrations)

\\\powershell
cd MVCProject
dotnet ef database update
\\\

### 4. Run the Application

\\\powershell
cd MVCProject
dotnet run
\\\

The application will start and display the URL in the console:

\\\
Now listening on: https://localhost:5001
\\\

Open your browser and navigate to \https://localhost:5001\

### 5. Using Visual Studio

1. Open \MVCProject.sln\ in Visual Studio
2. Right-click the project ? Set as Startup Project
3. Press \F5\ or click "Run"

## ?? Configuration

### appsettings.json

Main application configuration file:

\\\json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*"
}
\\\

### appsettings.Development.json

Development-specific overrides:

\\\json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information"
    }
  }
}
\\\

### Database Connection

Update the connection string in \ppsettings.json\:

\\\json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\\\mssqllocaldb;Database=MVCProjectDb;Trusted_Connection=true;"
}
\\\

## ??? Architecture

### MVC Pattern

- **Model**: Data structures in \Models/\ and \ViewModels/\
- **View**: Razor templates in \Views/\
- **Controller**: Request handlers in \Controllers/\

### Layered Architecture

1. **Presentation Layer** (Controllers & Views)
2. **Business Logic Layer** (Services & Repositories)
3. **Data Access Layer** (EF Core, DbContext)
4. **Database** (SQL Server)

## ??? Available Routes

| Route | Controller | Action | Description |
|-------|-----------|--------|-------------|
| \/\ | Home | Index | Home page |
| \/Home/Privacy\ | Home | Privacy | Privacy policy page |
| \/Home/Error\ | Home | Error | Error page |

**Route Pattern**: \/{controller=Home}/{action=Index}/{id?}\

### Static Assets

Static files are served from the \wwwroot/\ directory:

- CSS: \/css/\
- JavaScript: \/js/\
- Images: \/images/\
- Libraries: \/lib/\

## ?? Services

### Currently Configured

In \Program.cs\:

- **ControllersWithViews** - Enables MVC controller and view support
- **Authorization Middleware** - Enforces authorization policies

### Available for Configuration

- **Entity Framework Core** - Database operations
- **ASP.NET Core Identity** - User authentication and role management
- **Mapster** - Automatic object mapping between layers
- **Dependency Injection** - Built-in IoC container

### Example Service Registration

\\\csharp
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
\\\

## ?? Dependencies

### NuGet Packages

| Package | Version | Purpose |
|---------|---------|---------|
| \Mapster\ | 10.0.7 | Object mapping |
| \Microsoft.AspNetCore.Identity.EntityFrameworkCore\ | 10.0.5 | Identity integration with EF Core |
| \Microsoft.EntityFrameworkCore.SqlServer\ | 10.0.5 | SQL Server provider for EF Core |
| \Microsoft.EntityFrameworkCore.Design\ | 10.0.5 | EF Core design-time tools |
| \Microsoft.EntityFrameworkCore.Tools\ | 10.0.5 | EF Core command-line tools |

### Client-Side Libraries

Included in \wwwroot/lib/\:

- **Bootstrap** - Responsive CSS framework
- **jQuery** - JavaScript library
- **jQuery Validation** - Form validation
- **jQuery Validation Unobtrusive** - Unobtrusive validation

## ??? Database

### Entity Framework Core

This project uses EF Core with SQL Server. 

#### Create a New Migration

\\\powershell
dotnet ef migrations add MigrationName
\\\

#### Update Database

\\\powershell
dotnet ef database update
\\\

#### View Database Schema

\\\powershell
dotnet ef dbcontext info
\\\

## ?? Logging

Logging is configured via \ppsettings.json\:

\\\csharp
var logger = HttpContext.RequestServices.GetRequiredService<ILogger<HomeController>>();
logger.LogInformation("Information message");
logger.LogWarning("Warning message");
logger.LogError("Error message");
\\\

## ?? Security

- Use HTTPS in production
- Enable CORS only for trusted origins
- Validate all user input
- Use parameterized queries (EF Core handles this)
- Implement proper authentication and authorization
- Keep NuGet packages updated

## ?? Troubleshooting

### Port Already in Use

\\\powershell
# Change port in Properties/launchSettings.json
\\\

### Database Connection Issues

\\\powershell
# Verify connection string in appsettings.json
# Ensure SQL Server is running
# Check firewall settings
\\\

### NuGet Package Restore Fails

\\\powershell
dotnet nuget locals all --clear
dotnet restore
\\\

### Migrations Not Found

\\\powershell
# Ensure you're in the MVCProject directory
cd MVCProject
dotnet ef migrations add InitialCreate
\\\

## ?? Resources

- [Microsoft Learn - ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity/)
- [Mapster Documentation](https://mapperly.riok.app/)
- [Bootstrap Documentation](https://getbootstrap.com/docs/)

## ?? License

This project is licensed under the MIT License. See the LICENSE file for details.

## ????? Author

Created as an educational project for ASP.NET Core MVC development.

---

**Last Updated**: 2025
