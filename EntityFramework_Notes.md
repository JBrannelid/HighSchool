# Entity Framework Core - Personal Reference Guide

## Basic Database Operations

### Reading Data (Select)
```csharp
// Get all records
var allStudents = context.Students.ToList();

// Get single record
var student = context.Students.FirstOrDefault(s => s.Id == 1);

// Get filtered records
var activeStudents = context.Students
    .Where(s => s.IsActive == true)
    .ToList();
```

### Creating Data (Insert)
```csharp
// Create new record
var newStudent = new Student 
{
    FirstName = "John",
    LastName = "Doe"
};

// Add to context
context.Students.Add(newStudent);

// Save changes to database
context.SaveChanges();
```

### Updating Data (Update)
```csharp
// Find record to update
var student = context.Students.Find(1);

// Modify properties
student.FirstName = "Jane";

// Save changes
context.SaveChanges();
```

### Deleting Data (Delete)
```csharp
// Find record to delete
var student = context.Students.Find(1);

// Remove from context
context.Students.Remove(student);

// Save changes
context.SaveChanges();
```

## LINQ Operations

### Filtering (Where)
```csharp
// Basic filtering
var passedStudents = context.Grades
    .Where(g => g.Score >= 60)
    .ToList();

// Multiple conditions
var seniorHonorStudents = context.Students
    .Where(s => s.Grade == "12" && s.GPA >= 3.5)
    .ToList();
```

### Sorting (OrderBy)
```csharp
// Ascending order
var sortedByName = context.Students
    .OrderBy(s => s.LastName)
    .ToList();

// Descending order
var sortedByGradeDesc = context.Grades
    .OrderByDescending(g => g.Score)
    .ToList();

// Multiple sort criteria
var sorted = context.Students
    .OrderBy(s => s.Grade)
    .ThenBy(s => s.LastName)
    .ToList();
```

### Including Related Data (Include)
```csharp
// Include single relationship
var studentsWithClasses = context.Students
    .Include(s => s.Class)
    .ToList();

// Include multiple relationships
var studentsWithClassesAndGrades = context.Students
    .Include(s => s.Class)
    .Include(s => s.Grades)
    .ToList();

// Include nested relationships
var studentsWithDeepData = context.Students
    .Include(s => s.Class)
        .ThenInclude(c => c.Teacher)
    .ToList();
```

## Common Patterns

### Combining Operations
```csharp
var result = context.Students
    .Include(s => s.Class)
    .Where(s => s.GPA >= 3.0)
    .OrderBy(s => s.LastName)
    .ToList();
```

### Async Operations
```csharp
var students = await context.Students
    .Where(s => s.IsActive)
    .ToListAsync();
```

Remember: Always call `SaveChanges()` after modifying data (Insert/Update/Delete)!