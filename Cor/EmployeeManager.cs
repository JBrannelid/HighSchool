using HighSchool.Data;
using HighSchool.Models;
using Microsoft.EntityFrameworkCore;


/* EmployeeManager class handles all student-related CRUD operations with LINQ
 * Inherits from PersonManager for common functionality
 * Add/Edit/Delet return a bool value to allow UI to provide feedback to user base on sucess or not
 */

namespace HighSchool.Core
{
    public class EmployeeManager : PersonManager
    {
        private readonly HighSchoolContext _context;

        // Constructor takes database context and passes it to base class (PersonManager)
        public EmployeeManager(HighSchoolContext context) : base(context)
        {
            _context = context;
        }
        public bool AddEmployee(string firstName, string lastName, string pin,
                              string gender, int positionId)
        {
            return AddPerson(firstName, lastName, pin, gender, true, null, positionId);
        }

        public bool EditEmployee(int employeeId, string firstName, string lastName)
        {
            return EditPerson(employeeId, firstName, lastName);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return DeletePerson(employeeId);
        }

        // LINQ query return all person with roll true and include their position (Teacher, Principal, Administrator)
        public IEnumerable<Person> GetAllEmployees()
        {
            return _context.People
                .Include(p => p.Fkposition) // Return the person position details
                .Where(p => p.Role == true) // Return only employees
                .ToList();                       // Save filtration to IEnumerable List
        }

        // Used for position-specific employee listings and reports
        public IEnumerable<Person> GetEmployeesByPosition(int positionId)
        {
            return _context.People
                .Include(p => p.Fkposition) // Return the person position details
                .Where(p => p.Role == true && p.FkpositionId == positionId) // Return only employees with their positions 
                .ToList();                       // Save filtration to IEnumerable List
        }
    }
}