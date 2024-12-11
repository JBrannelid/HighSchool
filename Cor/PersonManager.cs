using HighSchool.Data;
using HighSchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


/* Abstract Class 
 * Reduces code repetition and improves maintainability
 * The UI services will handle boolean value True/False and display message to the console 
 */
namespace HighSchool.Core
{
    public abstract class PersonManager
    {
        protected readonly HighSchoolContext _context; // Instance of HighSchoolContext to enable database interaction

        protected PersonManager(HighSchoolContext context)

        {
            _context = context;  // Initializes the _context field from HighSchoolContext
        }

        // Add a new person (Student or employees). Validation error defines in the DB constraints 
        protected bool AddPerson(string firstName, string lastName, string pin,
                                 string gender, bool isEmployee, int? classId = null, int? positionId = null)
        {
            {
                try
                {
                    var person = new Person
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Pin = pin,
                        Gender = gender,
                        Role = isEmployee,
                        FkclassId = classId,
                        FkpositionId = positionId
                    };

                    // Adds the new person to the People table and saves the changes to the database through HighSchoolContext
                    _context.People.Add(person);
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException ex)
                {   // Handles database errors. Specific SQL error codes (2627 for duplicate PIN, 547 for constraint violations) are caught and handled.
                    if (ex.InnerException is SqlException sqlEx)
                    {
                        switch (sqlEx.Number)
                        {
                            case 2627: // Error msg:2627 Violation of unique constraint
                                if (sqlEx.Message.Contains("UC_PIN"))
                                    Console.WriteLine("Error: Personnumret finns redan registrerat.");
                                break;

                            case 547: // 547 is the error code used for any constraint violation
                                if (sqlEx.Message.Contains("CHK_ValidGender"))
                                    Console.WriteLine("Error: Ogiltigt kön angivet. Använd Male, Female eller Other.");
                                else if (sqlEx.Message.Contains("CHK_ValidPIN"))
                                    Console.WriteLine("Error: Ogiltigt personnummerformat. Använd YYYYMMDD-XXXX.");
                                else if (sqlEx.Message.Contains("FK_People_Class"))
                                    Console.WriteLine("Error: Ogiltig klass vald.");
                                break;

                            default: // Default if an error code is thrown that haven't pick error msg: 2627 or 547
                                Console.WriteLine($"Ett databasfel uppstod: {sqlEx.Message}");
                                break;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(value: $"Ett databasfel uppstod: {ex}");
                    return false; // Catch all exceptions in the compiler and throw to the user
                }
            }
        }
        // Method to update a person's name based on their ID.  If any of the names are empty or null, they will not be updated.
        protected bool EditPerson(int personId, string firstName, string lastName)
        {
            try
            {
                var person = _context.People.Find(personId);
                if (person == null) return false;

                if (!string.IsNullOrWhiteSpace(firstName))
                    person.FirstName = firstName;
                if (!string.IsNullOrWhiteSpace(lastName))
                    person.LastName = lastName;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                return false;
            }
        }
        // Method to delete a person's name based on their ID.
        protected bool DeletePerson(int personId)
        {
            try
            {
                var person = _context.People.Find(personId);
                if (person == null) return false;

                // Saves the changes to the database through HighSchoolContext
                _context.SaveChanges();
                return true;
            }
            // If the person is not found. If deletion fails, an exception is caught and an error message is displayed.
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
                return false;
            }
        }
    }
}