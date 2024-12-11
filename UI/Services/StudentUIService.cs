using HighSchool.Core;

namespace HighSchool.UI.Services
{   // Handles all student-related UI operations and user interactions
    public class StudentUIService
    {
        private readonly StudentManager _studentManager; // Manages student-related operations (from Core layer)
        private readonly FormatHelpers _formatHelpers; // Provides utility functions for formatting data

        // Constructor uses dependency injection to maintain loose coupling
        public StudentUIService(StudentManager studentManager, FormatHelpers formatHelpers)
        {
            _studentManager = studentManager;
            _formatHelpers = formatHelpers;
        }

        // Main entry point for student management operations. Routes student management operations to private Methods 
        public void HandleStudentMenu(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        AddStudentUI();
                        break;

                    case "2":
                        EditStudentUI();
                        break;

                    case "3":
                        DeleteStudentUI();
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett oväntat fel uppstod: {ex.Message}");
            }
        }

        // Displays sorted student list by using formatHelper for consistent name formatting
        public void DisplayStudents(string sortChoice)
        {
            Console.Clear();
            var students = _studentManager.GetSortedStudents(sortChoice);

            Console.WriteLine("\nElever:"); // Header
            foreach (var student in students)
            {
                Console.WriteLine(_formatHelpers.FormatStudentName(student, sortChoice));
            }
        }

        // Shows students filtered by class
        public void DisplayClassStudents(string classChoice, string sortChoice)
        {
            if (int.TryParse(classChoice, out int classId))
            {
                var students = _studentManager.GetStudentsByClass(classId);
                foreach (var student in students)
                {   // Includes null check on Fkclass for data integrity
                    Console.WriteLine($"{_formatHelpers.FormatStudentName(student, sortChoice)} - {student.Fkclass?.ClassName}");
                }
            }
        }

        // Handles new student registration by a private method as it's only called internally via HandleStudentMenu
        private void AddStudentUI()
        {
            Console.WriteLine("\n=== Lägg till ny elev ===\n");

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Personnummer (YYYYMMDD-XXXX): ");
            string pin = Console.ReadLine();

            Console.Write("Kön (Male/Female/Other): ");
            string gender = Console.ReadLine();

            Message.ShowClassSelectionMenu();
            if (!int.TryParse(Console.ReadLine(), out int classId))
            {
                Console.WriteLine("Ogiltig klass!");
                return;
            }

            // Validation is done thourgh Constrains in DB. Recive a bool value if sucess or not from AddStudent
            if (_studentManager.AddStudent(firstName, lastName, pin, gender, classId))
                Console.WriteLine("Student tillagd!");
            else
                Console.WriteLine("Kunde inte lägga till student.");
        }

        // Handles student information updates. Display current students first for better UX
        private void EditStudentUI()
        {
            Console.WriteLine("\n=== Redigera elev ===\n");

            var students = _studentManager.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.PersonId} - {student.FirstName} {student.LastName} - Klass: {student.Fkclass?.ClassName}");
            }

            Console.Write("\nAnge ID på eleven som ska redigeras: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            // Allows empty inputs to keep existing values
            Console.Write("Nytt förnamn (lämna tomt för att behålla nuvarande): ");
            string firstName = Console.ReadLine();

            // Allows empty inputs to keep existing values
            Console.Write("Nytt efternamn (lämna tomt för att behålla nuvarande): ");
            string lastName = Console.ReadLine();

            if (_studentManager.EditStudent(studentId, firstName, lastName))
                Console.WriteLine("Student uppdaterad!");
            else
                Console.WriteLine("Kunde inte uppdatera student.");
        }

        // Handles student removal from DB with confirmation prompt
        private void DeleteStudentUI()
        {
            Console.WriteLine("\n=== Ta bort elev ===\n");

            // Shows current students for reference
            var students = _studentManager.GetAllStudents();
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.PersonId} - {student.FirstName} {student.LastName} - Klass: {student.Fkclass?.ClassName}");
            }

            Console.Write("\nAnge ID på eleven som ska tas bort: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            // Requires explicit confirmation for safety
            Console.Write("Är du säker på att du vill ta bort denna elev? (j/n): ");
            if (Console.ReadLine()?.ToLower() == "j")
            {
                if (_studentManager.DeleteStudent(studentId))
                    Console.WriteLine("Student borttagen!");
                else
                    Console.WriteLine("Kunde inte ta bort student.");
            }
        }
    }
}