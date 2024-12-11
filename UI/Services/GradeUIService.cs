using HighSchool.Core;

namespace HighSchool.UI.Services
{   // Manages grade-related UI operations and data presentation
    public class GradeUIService
    {
        private readonly GradeManager _gradeManager; // Manages grade-related operations (from Core layer)
        private readonly FormatHelpers _formatHelpers; // Provides utility functions for formatting data

        // Constructor uses dependency injection to maintain loose coupling
        public GradeUIService(GradeManager gradeManager, FormatHelpers formatHelpers)
        {
            _gradeManager = gradeManager;
            _formatHelpers = formatHelpers;
        }

        // Displays recent grades in table format
        public void DisplayRecentGrades()
        {
            Console.Clear();
            var recentGrades = _gradeManager.GetRecentGrades(); // Retrives grades from SQL Server view VmRecentGrade

            // Fallback if no grade i set last month 
            if (!recentGrades.Any())
            {
                Console.WriteLine("\nInga betyg har satts den senaste månaden.");
                return;
            }

            Console.WriteLine("\n=== Senaste månadens betyg ===\n");

            string format = "{0,-25} {1,-15} {2,-8} {3,-20} {4,-12}"; // Fixed width columns for alignment
            Console.WriteLine(format, "Student", "Kurs", "Betyg", "Lärare", "Datum"); // Table header
            Console.WriteLine(new string('-', 85)); // Table Headliner 

            // Display each grade with formatted date
            foreach (var grade in recentGrades)
            {
                // Uses formatHelper for consistent number formatting
                string date = _formatHelpers.FormatGradeAssignedDate(grade.GradeAssignedDate);
                Console.WriteLine(format,
                    grade.StudentName,
                    grade.CourseName,
                    grade.Grade,
                    grade.TeacherName,
                    date
                );
            }

            Console.WriteLine(new string('-', 85)); // Table subliner 
        }

        // Shows statistical overview of course performance
        public void DisplayCourseStatistics()
        {
            Console.Clear();
            var statistics = _gradeManager.GetCourseStatistics(); // Retrives statistics from SQL Server view VwCourseStatistic

            Console.WriteLine("\n=== Kursstatistik ===\n");

            string format = "{0,-25} {1,-15} {2,-8} {3,-20} {4,-12}"; // Matches grade display format
            Console.WriteLine(format, "Kursnamn", "Antal Betyg", "Medelbetyg", "Högsta Betyg", "Lägsta Betyg"); // Table header
            Console.WriteLine(new string('-', 85)); // Table headliner

            // Format and display each course's statistics by going through SQL-view
            foreach (var stat in statistics)
            {
                // Uses formatHelper for consistent number formatting
                Console.WriteLine(_formatHelpers.FormatCourseStatistics(
                    stat.CourseName,
                    stat.NumberOfGrades,
                    stat.AverageGrade,
                    stat.HighestGrade,
                    stat.LowestGrade
                ));
            }

            Console.WriteLine(new string('-', 85)); // Table subliner 
        }
    }
}