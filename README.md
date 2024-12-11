# High School Management System

## Overview
A C# console application for managing a high school database using Entity Framework Core with a Database First approach. The application demonstrates basic database operations, input validation, and data presentation through a console interface.

## Features
- Student Management
- Employee Management
- Grade Management

## Technical Stack
- C# (.NET 8.0)
- Entity Framework Core (9.0.0)
- SQL Server
- Console-based UI

## Prerequisites
- Visual Studio 2022 or newer
- SQL Server (Local or Express)
- .NET 8.0 SDK

## Required Packages
```shell
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design

Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Json
```

## Installation & Setup

### 1. Database Setup
```bash
# Option 1: Using SQL Server Management Studio (SSMS)
1. Open SSMS
2. Connect to your SQL Server instance
3. Open the file: ./SQL/SQLQuery_Labb3.sql
4. Execute the script

# Option 2: Using Command Line
sqlcmd -S YOUR_SERVER -i ./SQL/SQLQuery_Labb3.sql
```

### 2. Application Setup
1. Clone the repository
```bash
git clone https://github.com/JBrannelid/HighSchool.git
cd highschool-management
```

2. Configure the database connection
   - Change Filename `appsettings.example.json` to `appsettings.json`
   - Update the connection string in `appsettings.json` with your database details:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=YOUR_SERVER;Database=HighSchool;Trusted_Connection=True;TrustServerCertificate=True;"
       }
     }
     ```

3. Build and run
```bash
dotnet build
dotnet run
```

## Project Structure
```
HighSchool/
├── Core/                   # Business logic and data operations
├── Data/                   # Database context and configuration
├── Models/                 # Entity models
├── SQL/                    # Database scripts
├── UI/                     # User interface components
└── Program.cs              # Application entry point
```

## Database Structure
- People (Students and Staff with personal information)
- Classes (Class groups like 1A, 1B, etc.)
- Positions (Teacher, Principal, Administrator)
- Courses (Available subjects)
- CourseEnrollments (Student-Course connections with grades)
- GradeValues (Grade scale definition)
- Views:
  - Course Statistics
  - Recent Grades

## Console
The application uses a clear console-based interface for easy data visualization. Here's an example of the grade display:

```
=== Senaste månadens betyg ===

Student                   Kurs            Betyg    Lärare               Datum
-------------------------------------------------------------------------------------
Erik Johansson            Matematik       A        Lars Svensson        2024-12-06
Olof Eriksson             Fysik           B        Johan Karlsson       2024-12-01
David Jonsson             Matematik       A        Lars Svensson        2024-12-01
Maria Karlsson            Kemi            C        Eva Nilsson          2024-11-28
Marcus Olsson             Fysik           C        Johan Karlsson       2024-11-26
-------------------------------------------------------------------------------------
Tryck på valfri tangent för att fortsätta...
```

