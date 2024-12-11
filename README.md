# High School Management System

## Overview
A C# console application for managing a high school database using Entity Framework Core with a Database First approach

## Features
- Student Management (Add/Edit/Delete/View)
- Employee Management (Teachers, Administrators, Principal)
- Grade Tracking and Statistics
- Class-based Student Organization
- Basic Data Analysis and Reporting

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
```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design

Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Json
```

## Installation
1. Clone the repository
```bash
git clone https://github.com/jbrannelid/highschool-management.git
```

2. Configure the database connection
   - Copy `appsettings.example.json` to `appsettings.json`
   - Update the connection string in `appsettings.json` with your database details:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;"
       }
     }
     ```

3. Build and run the application
```bash
dotnet build
dotnet run
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

## Usage
The application provides a menu-driven interface for:
- Viewing and managing students
- Managing employee records
- Viewing grades and statistics
- Generating basic reports

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