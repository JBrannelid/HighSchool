using HighSchool.Core;
using HighSchool.Models;

namespace HighSchool.UI.Services
{    // Handles all Employee-related UI operations and user interactions
    public class EmployeeUIService
    {   
        private readonly EmployeeManager _employeeManager;
        private readonly FormatHelpers _formatHelpers;

        // Constructor uses dependency injection to maintain loose coupling
        public EmployeeUIService(EmployeeManager employeeManager, FormatHelpers formatHelpers)
        {
            _employeeManager = employeeManager;
            _formatHelpers = formatHelpers;
        }

        // Displays filtered employee lists based on position codes
        public void DisplayEmployees(string sortChoice)
        {
            IEnumerable<Person> employees;
            switch (sortChoice)
            {
                case "2": // Teachers
                    employees = _employeeManager.GetEmployeesByPosition(100); // Teacher
                    break;

                case "3": // Administrator
                    employees = _employeeManager.GetEmployeesByPosition(102); // Administrator
                    break;

                case "4": // Principal
                    employees = _employeeManager.GetEmployeesByPosition(101); // Principal
                    break;

                default:
                    employees = _employeeManager.GetAllEmployees();
                    break;
            }

            Console.WriteLine("\nPersonal:"); // Header
            foreach (var employee in employees)
            {   // Position can be nulll
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.Fkposition?.PositionName}");
            }
        }

        // Main entry point for Employees management operations. Routes Employee management operations to private Methods 
        public void HandleEmployeeMenu(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        AddEmployeeUI();
                        break;

                    case "2":
                        EditEmployeeUI();
                        break;

                    case "3":
                        DeleteEmployeeUI();
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }

        // Handles new Employee registration by a private method as it's only called internally via HandleEmployeeMenu
        private void AddEmployeeUI()
        {
            Console.WriteLine("\n=== Lägg till ny personal ===\n");

            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();

            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();

            Console.Write("Personnummer (YYYYMMDD-XXXX): ");
            string pin = Console.ReadLine();

            Console.Write("Kön (Male/Female/Other): ");
            string gender = Console.ReadLine();

            Message.ShowPositionsMenu();
            if (!int.TryParse(Console.ReadLine(), out int positionId))
            {
                Console.WriteLine("Ogiltig position!");
                return;
            }

            // Validation is done thourgh Constrains in DB. Recive a bool value if sucess or not
            if (_employeeManager.AddEmployee(firstName, lastName, pin, gender, positionId))
                Console.WriteLine("Personal tillagd!");
            else
                Console.WriteLine("Kunde inte lägga till personal.");
        }

        // Updates employee data with null checks for unchanged fields
        private void EditEmployeeUI()
        {
            Console.WriteLine("\n=== Redigera personal ===\n");

            var employees = _employeeManager.GetAllEmployees();

            foreach (var employee in employees)
            {   // Fkposition can be NULL
                Console.WriteLine($"ID: {employee.PersonId} - {employee.FirstName} {employee.LastName} - Position: {employee.Fkposition?.PositionName}");
            }

            Console.Write("\nAnge ID på personal som ska redigeras: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            Console.Write("Nytt förnamn (lämna tomt för att behålla nuvarande): ");
            string firstName = Console.ReadLine();

            Console.Write("Nytt efternamn (lämna tomt för att behålla nuvarande): ");
            string lastName = Console.ReadLine();

            if (_employeeManager.EditEmployee(employeeId, firstName, lastName))
                Console.WriteLine("Personal uppdaterad!");
            else
                Console.WriteLine("Kunde inte uppdatera personal.");
        }

        // Handles Employee removal from DB with confirmation prompt
        private void DeleteEmployeeUI()
        {
            Console.WriteLine("\n=== Ta bort personal ===\n");

            var employees = _employeeManager.GetAllEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.PersonId} - {employee.FirstName} {employee.LastName} - Position: {employee.Fkposition?.PositionName}");
            }

            Console.Write("\nAnge ID på personal som ska tas bort: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            Console.Write("Är du säker på att du vill ta bort denna personal? (j/n): ");
            if (Console.ReadLine()?.ToLower() == "j")
            {
                if (_employeeManager.DeleteEmployee(employeeId))
                    Console.WriteLine("Personal borttagen!");
                else
                    Console.WriteLine("Kunde inte ta bort personal.");
            }
        }
    }
}