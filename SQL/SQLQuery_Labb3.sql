-- Johannes Brannelid
-- Chas Academy
-- LAB 3 (SQL & Object-Relational Mapping)
-- Databases

CREATE DATABASE HighSchool
GO

USE HighSchool
GO

-- Create a class table with Constraints for year (1-3) and sections (A,B,C)
-- Every class have their own uniq ClassId
CREATE TABLE Classes (
    ClassId INT IDENTITY(1,1) PRIMARY KEY,
    ClassName NVARCHAR(10) UNIQUE NOT NULL,
    Year INT NOT NULL,
    Section CHAR(1) NOT NULL,
    CONSTRAINT CHK_ValidYear CHECK (Year BETWEEN 1 AND 3),
    CONSTRAINT CHK_ValidSection CHECK (Section IN ('A', 'B', 'C'))
)
GO

-- Insert Classes
INSERT INTO Classes (ClassName, Year, Section)
VALUES 
('1A', 1, 'A'),
('1B', 1, 'B'),
('1C', 1, 'C'),
('2A', 2, 'A'),
('2B', 2, 'B'),
('2C', 2, 'C'),
('3A', 3, 'A'),
('3B', 3, 'B'),
('3C', 3, 'C')
GO

-- Create Positions table that will handle the employees rol position 
CREATE TABLE Positions (
    PositionId INT IDENTITY(100,1) PRIMARY KEY,
    PositionName NVARCHAR(50) UNIQUE NOT NULL
)
GO

-- As of now, employees can hold three positions. This will be scalable in the future
INSERT INTO Positions (PositionName)
VALUES ('Teacher'), ('Principal'), ('Administrator')
GO

-- Create the GradeValues table with predefined values
-- Use it for statistical calculations on set grades for the high school
-- Add a constraint so that each grade value must be unique
CREATE TABLE GradeValues (
    Grade CHAR(2) PRIMARY KEY,
    GradeValue INT NOT NULL,
    CONSTRAINT UC_GradeValue UNIQUE (GradeValue)
)
GO

-- Insert grade values
INSERT INTO GradeValues (Grade, GradeValue)
VALUES 
('A', 5),
('B', 4),
('C', 3),
('D', 2),
('E', 1),
('F', 0)
GO

-- Create People table with constraints for a safer datastorage 
CREATE TABLE People (
    PersonId INT IDENTITY(100,1) PRIMARY KEY,
    FirstName NVARCHAR(200) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    PIN NVARCHAR(13) NOT NULL,						-- Personal Information Number (PIN)
    Role BIT NOT NULL DEFAULT 0,					-- 0 = Student, 1 = Staff (student is Default)
    FKClassId INT NULL,								-- Only for students
    FKPositionId INT NULL,							-- Only for staff

    CONSTRAINT FK_People_Class FOREIGN KEY (FKClassId) 
        REFERENCES Classes(ClassId),
    CONSTRAINT FK_People_Position FOREIGN KEY (FKPositionId) 
        REFERENCES Positions(PositionId),
    CONSTRAINT UC_PIN UNIQUE (PIN),													-- Every PIN has to be Unique 
    CONSTRAINT CHK_ValidGender CHECK (Gender IN ('Male', 'Female', 'Other')),		-- Constraint for Gender

    CONSTRAINT CHK_ValidPIN CHECK (													-- Validate PIN	Constraint
		-- Extract PIN-format (YYYYMMDD-XXXX)
		PIN LIKE '[0-9][0-9][0-9][0-9][0-1][0-9][0-3][0-9]-[0-9][0-9][0-9][0-9]'
		-- Check if month is between 01-12
        AND SUBSTRING(PIN, 5, 2) BETWEEN '01' AND '12'
		-- Check if days is between 01-21
		AND SUBSTRING(PIN, 7, 2) BETWEEN '01' AND '31'
	)
)
GO

-- Create Courses table
CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(250) NOT NULL,
    CourseCode NVARCHAR(10) NOT NULL UNIQUE -- CourseCode most be Unique (Ex. MA101)
)
GO

-- Create CourseEnrollments table with grade validation
CREATE TABLE CourseEnrollments (
    EnrollmentId INT IDENTITY(100,1) PRIMARY KEY,
    Grade CHAR(2) NULL,
    GradeAssignedDate DATETIME NULL,
    FKStudentId INT NOT NULL,
    FKCourseId INT NOT NULL,
    FKTeacherId INT NULL,

	-- Set different CONSTRAINT that will validate conditions
    CONSTRAINT FK_Enrollment_Student FOREIGN KEY (FKStudentId) 
        REFERENCES People(PersonId),
    CONSTRAINT FK_Enrollment_Course FOREIGN KEY (FKCourseId) 
        REFERENCES Courses(CourseId),
    CONSTRAINT FK_Enrollment_Teacher FOREIGN KEY (FKTeacherId) 
        REFERENCES People(PersonId),
    CONSTRAINT FK_Enrollment_Grade FOREIGN KEY (Grade) 
        REFERENCES GradeValues(Grade),

	-- Check that a grade has a assigned date 
    CONSTRAINT CHK_GradeAndDate CHECK (
        (Grade IS NULL AND GradeAssignedDate IS NULL) OR
        (Grade IS NOT NULL AND GradeAssignedDate IS NOT NULL)
))
GO


----------------- Insert sample data for Courses, People, and CourseEnrollments ------------------
----------------- All personal data is generated and does not represent real information -----------



-- Insert sample courses
INSERT INTO Courses (CourseName, CourseCode) VALUES
('Matematik', 'MA101'),			-- Mathematics
('Fysik', 'FY101'),				-- Physics
('Kemi', 'KE101'),				-- Chemistry
('Biologi', 'BI101'),			-- Biology
('Svenska', 'SV101'),			-- Swedish
('Engelska', 'EN101'),			-- English
('Historia', 'HI101'),			-- History
('Geografi', 'GE101'),			-- Geography
('Idrott och hälsa', 'IH101');  -- Physical Education
GO

-- Insert sample students and staff 
INSERT INTO People (FirstName, LastName, Gender, PIN, Role, FKClassId, FKPositionId) VALUES
-- students
('Anna', 'Svensson', 'Female', '20010101-1234', 0, '1', NULL),		-- Student, Role = 0, Class = 1A
('Erik', 'Johansson', 'Female', '20020202-2345', 0, '2', NULL),		-- Student, Role = 0, Class = 1B
('Maria', 'Karlsson', 'Female', '20030303-3456', 0, '4', NULL),		-- Student, Role = 0, Class = 2A
('Johan', 'Lindberg', 'Male', '20040404-4567', 0, '5', NULL),		-- Student, Role = 0, Class = 2B
('Elin', 'Nilsson', 'Female', '20050505-5678', 0, '7', NULL),		-- Student, Role = 0, Class = 3A
('Lina', 'Persson', 'Female', '20060606-6789', 0, '8', NULL),		-- Student, Role = 0, Class = 3B
('Marcus', 'Olsson', 'Male', '20070707-7890', 0, '1', NULL),		-- Student, Role = 0, Class = 1A
('Sara', 'Eriksson', 'Female', '20080808-8901', 0, '5', NULL),		-- Student, Role = 0, Class = 2B
('David', 'Jonsson', 'Male', '20090909-9012', 0, '9', NULL),		-- Student, Role = 0, Class = 3C
('Frida', 'Persson', 'Female', '20100101-0123', 0, '2', NULL),		-- Student, Role = 0, Class = 1B
('Oscar', 'Larsson', 'Male', '20110202-1234', 0, '4', NULL),		-- Student, Role = 0, Class = 2A
('Ida', 'Sjöberg', 'Female', '20120303-2345', 0, '3', NULL),		-- Student, Role = 0, Class = 1C
('Lucas', 'Lindström', 'Male', '20130404-3456', 0, '5', NULL),		-- Student, Role = 0, Class = 2B
('Elisabeth', 'Holm', 'Female', '20140505-4567', 0, '7', NULL),		-- Student, Role = 0, Class = 3A
('Viktor', 'Gustafsson', 'Male', '20150606-5678', 0, '1', NULL)		-- Student, Role = 0, Class = 1A
GO

INSERT INTO People (FirstName, LastName, Gender, PIN, Role, FKClassId, FKPositionId) VALUES
-- Principal
('Olof', 'Eriksson', 'Male', '19790101-1234', 1, NULL, 101)			-- Principal, Role = 1, FKPositionId = 101 (Principal)
GO

INSERT INTO People (FirstName, LastName, Gender, PIN, Role, FKClassId, FKPositionId) VALUES
-- Administrators
('Maria', 'Johansson', 'Female', '19820515-2345', 1, NULL, 102),	-- Administrator, Role = 1, FKPositionId = 102
('Karin', 'Lindberg', 'Female', '19850325-3456', 1, NULL, 102)		-- Administrator, Role = 1, FKPositionId = 102
GO

INSERT INTO People (FirstName, LastName, Gender, PIN, Role, FKClassId, FKPositionId) VALUES
-- Teachers
('Lars', 'Svensson', 'Male', '19891212-4567', 1, NULL, 100),		-- Teacher, Role = 1, FKPositionId = 100
('Eva', 'Nilsson', 'Female', '19900506-5678', 1, NULL, 100),		-- Teacher, Role = 1, FKPositionId = 100
('Johan', 'Karlsson', 'Male', '19910420-6789', 1, NULL, 100),		-- Teacher, Role = 1, FKPositionId = 100
('Anna', 'Gustafsson', 'Female', '19920214-7890', 1, NULL, 100),	-- Teacher, Role = 1, FKPositionId = 100
('Per', 'Andersson', 'Male', '19930430-8901', 1, NULL, 100)			-- Teacher, Role = 1, FKPositionId = 100
GO

-- Insert sample enrollments with grades
-- Diffrent set values for fun. Testing with GATEDATE and hard coding values
INSERT INTO CourseEnrollments (FKStudentId, FKCourseId, FKTeacherId, Grade, GradeAssignedDate) VALUES
(101, 1, 118, 'A', GETDATE()),   
(115, 2, 120, 'B', DATEADD(DAY, -5, GETDATE())), 
(102, 3, 119, 'C', DATEADD(DAY, -8, GETDATE())),
(103, 4, 121, 'B', DATEADD(DAY, -58, GETDATE())),   
(104, 5, 121, 'A', DATEADD(DAY, -88, GETDATE())),
(105, 1, 118, 'D', DATEADD(DAY, -30, GETDATE())),
(106, 2, 120, 'C', DATEADD(DAY, -10, GETDATE())),
(107, 3, 119, 'B', DATEADD(MONTH, -1, GETDATE())),
(108, 4, 121, 'A', DATEADD(MONTH, -1, GETDATE())),   
(109, 5, 121, 'F', DATEADD(MONTH, -1, GETDATE())), 
(104, 2, 104, 'B', DATEADD(MONTH, -1, GETDATE())),   
(102, 4, 121, 'A', DATEADD(MONTH, -2, GETDATE())),  
(105, 5, 121, 'C', DATEADD(MONTH, -2, GETDATE())),  
(107, 2, 120, 'D', DATEADD(MONTH, -2, GETDATE())),  
(108, 1, 118, 'A', '2024-12-01'),    
(106, 3, 119, 'C', '2023-05-30')
GO

----------------- VIEWS -----------------

-- View for lasth month grades
-- ALTER, added TeacherName
ALTER VIEW vw_RecentGrades AS
SELECT 
    p.FirstName + ' ' + p.LastName AS StudentName,
    c.CourseName,
    ce.Grade,
    gv.GradeValue,
    ce.GradeAssignedDate,
    t.FirstName + ' ' + t.LastName AS TeacherName
FROM CourseEnrollments ce
JOIN People p ON ce.FKStudentId = p.PersonId
JOIN Courses c ON ce.FKCourseId = c.CourseId
JOIN GradeValues gv ON ce.Grade = gv.Grade
JOIN People t ON ce.FKTeacherId = t.PersonId
WHERE ce.GradeAssignedDate >= DATEADD(MONTH, -1, GETDATE())
GO

-- View for gradestatistics
-- CREATE or ALTER for earlier changes. This view will CREATE or ALTER depending on the existence of the view
CREATE OR ALTER VIEW vw_CourseStatistics AS
SELECT 
    c.CourseName,
    COUNT(ce.Grade) as NumberOfGrades,
    AVG(CAST(gv.GradeValue AS FLOAT)) as AverageGrade,
    -- Använder MAX/MIN på GradeValue för att hitta rätt betyg
    (SELECT Grade FROM GradeValues WHERE GradeValue = MAX(gv.GradeValue)) as HighestGrade,
    (SELECT Grade FROM GradeValues WHERE GradeValue = MIN(gv.GradeValue)) as LowestGrade
FROM Courses c
LEFT JOIN CourseEnrollments ce ON c.CourseId = ce.FKCourseId
LEFT JOIN GradeValues gv ON ce.Grade = gv.Grade
GROUP BY c.CourseName;
GO

----------------- TEST SQL QUERY -----------------


-- Alternativ 1: Lista alla views
SELECT * FROM sys.views

-- Hämta senaste månaden betyg
SELECT * FROM vw_RecentGrades;

-- Hämta statistik per kurs
SELECT * FROM vw_CourseStatistics;

-- Visa betyg från senaste månaden
SELECT * FROM vw_RecentGrades 
WHERE GradeAssignedDate >= DATEADD(MONTH, -1, GETDATE())
ORDER BY GradeAssignedDate ASC

-- Sortering på view exempel
SELECT * FROM vw_CourseStatistics 
ORDER BY AverageGrade DESC;

----------------- SQL Queries--------------------
--[X]  Hämta alla lärare (dvs. inte personal med andra befattningar)
SELECT FirstName, LastName, PIN, PositionName 
FROM People
JOIN Positions ON FKPositionId = PositionId
WHERE role = 1
AND PositionName = 'Teacher';

--[x]  Hämta alla studenter i bokstavsordning, sorterat på efternamn
SELECT FirstName, LastName, PIN, Class
FROM People
WHERE Role = 0
ORDER BY LastName ASC

--[x]  Hämta alla studenter i en viss klass
SELECT FirstName, LastName, PIN, Class
FROM People
WHERE Role = 0 AND Class = '2B'

