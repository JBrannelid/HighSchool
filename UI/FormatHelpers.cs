using HighSchool.Models;

/* Format Helper
 * Main focus is to give the user a better userexperience
 * Uses of Ternary operator for a better code readability 
 */

namespace HighSchool.UI
{
    public class FormatHelpers
    {
        // Format the user First- and Lastname order for a better UX
        public string FormatStudentName(Person student, string sortChoice)
        {
            return (sortChoice == "1" || sortChoice == "2")
                ? $"{student.FirstName} {student.LastName}"
                : $"{student.LastName}, {student.FirstName}";
        }

        public string FormatGradeAssignedDate(DateTime? gradeAssignedDate)
        {
            return gradeAssignedDate.HasValue
                ? gradeAssignedDate.Value.ToString("yyyy-MM-dd") // Formate DateTime to xxxx-xx-xx
                : "N/A"; // If null value, print N/A for a better UX 
        }

        // Format a nullable grade as a string, with a fallback value N/A
        public string FormatGrade(double? grade)
        {
            return grade.HasValue
                ? grade.Value.ToString("0.00") // Format the grade to 2 decimal 
                : "N/A";
        }

        // Format a nullable number (e.g., Number of Grades) with a fallback value
        public string FormatNumber(int? number)
        {
            return number.HasValue ? number.Value.ToString() : "N/A";
        }

        // Format a string with consistent spacing for UX
        public string FormatCourseStatistics(string courseName, int? numberOfGrades, double? averageGrade, string highestGrade, string lowestGrade)
        {
            // Define the format for the columns
            string format = "{0,-25} {1,-15} {2,-8} {3,-20} {4,-12}";
            return string.Format(format,
                courseName,
                FormatNumber(numberOfGrades),
                FormatGrade(averageGrade),
                highestGrade ?? "N/A", 
                lowestGrade ?? "N/A"
            );
        }
    }
}