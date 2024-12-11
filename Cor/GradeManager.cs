using HighSchool.Data;
using HighSchool.Models;

/*  Manages all grade-related operations using LINQ.
 *  Uses database views for grade reporting and course statistics.
 */

namespace HighSchool.Core
{
    public class GradeManager 
    {
        private readonly HighSchoolContext _context; // Database context for accessing grade-related views

        public GradeManager(HighSchoolContext context)
        {
            _context = context;
        }

        // Retrieves grades from the last month using VwRecentGrade Views
        public IEnumerable<VwRecentGrade> GetRecentGrades()
        {
            return _context.VwRecentGrades
                .OrderByDescending(g => g.GradeAssignedDate) // Order By Desc from SQL View
                .ToList();
        }
        // Retrieves statistical data about course where grade higligt courses that doing good or poor performance  
        public IEnumerable<VwCourseStatistic> GetCourseStatistics()
        {
            return _context.VwCourseStatistics
                .OrderByDescending(a => a.AverageGrade) // Orders by average grade to highlight highest performing courses

                .ToList();
        }
    }
}