using HighSchool.Data;
using HighSchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


/* StudentManager class handles all student-related CRUD operations with LINQ
 * Inherits from PersonManager for common functionality
 * Add/Edit/Delete student returns true if successful, false if error occurs. UI-folder will handle the respons
 */
namespace HighSchool.Core
{
    public class StudentManager : PersonManager
    {
        private readonly HighSchoolContext _context;

        // Constructor takes database context and passes it to base class (PersonManager)
        public StudentManager(HighSchoolContext context) : base(context)
        {
            _context = context;
        }

        public bool AddStudent(string firstName, string lastName, string pin,
                             string gender, int classId)
        {
            return AddPerson(firstName, lastName, pin, gender, false, classId);
        }

        // Update Student First- and Lastname
        public bool EditStudent(int studentId, string firstName, string lastName)
        {
            return EditPerson(studentId, firstName, lastName);
        }

        // Removes a student from database or return false if no student is found by ID
        public bool DeleteStudent(int studentId)
        {
            return DeletePerson(studentId);
        }

        // Return all student, include their Class when GetAllStudnets is cald 
        public IEnumerable<Person> GetAllStudents()
        {
            return _context.People
                .Include(p => p.Fkclass)
                .Where(p => p.Role == false)
                .ToList();
        }
        // Returns all students in a specific class
        public IEnumerable<Person> GetStudentsByClass(int classId)
        {
            return _context.People
                .Include(p => p.Fkclass)
                .Where(p => p.Role == false && p.FkclassId == classId)
                .ToList();
        }
        // Returns students sorted by:
        // 1: FirstName ASC, 2: FirstName DESC, 3: LastName ASC, 4: LastName DESC
        public IEnumerable<Person> GetSortedStudents(string sortOrder) 
        {   
            var students = _context.People.Where(p => p.Role == false);

            switch (sortOrder)
            {
                case "1":
                    return students.OrderBy(s => s.FirstName).ToList();
                case "2":
                    return students.OrderByDescending(s => s.FirstName).ToList();
                case "3":
                    return students.OrderBy(s => s.LastName).ToList();
                case "4":
                    return students.OrderByDescending(s => s.LastName).ToList();
                default:
                    return students.OrderBy(s => s.LastName).ToList();
            }
        }
    }
}