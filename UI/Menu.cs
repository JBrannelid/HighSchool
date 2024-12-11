using HighSchool.UI;
using HighSchool.UI.Services;
using HighSchool.Core;
using HighSchool.Data;

// Main menu class handling Console interface for navigation between different menus
public class Menu
{   // UI service instances for every UI components 
    private readonly StudentUIService _studentUIService;
    private readonly EmployeeUIService _employeeUIService;
    private readonly GradeUIService _gradeUIService;

    // Constructor that initializes Db-connection and different UI components 
    public Menu()
    {
        // Create database context
        var context = new HighSchoolContext();

        // Create format helper
        var formatHelper = new FormatHelpers();

        // Create UI services with their required managers components 
        _studentUIService = new StudentUIService(new StudentManager(context), formatHelper);
        _employeeUIService = new EmployeeUIService(new EmployeeManager(context), formatHelper);
        _gradeUIService = new GradeUIService(new GradeManager(context), formatHelper);
    }

    public void ShowMainMenu()
    {
        bool running = true;
        while (running)
        {
            Message.DisplayMenueChoice();

            Console.Write("\nVälj ett alternativ: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Message.ShowEmployeesMenu();
                    string staffChoice = Console.ReadLine();

                    _employeeUIService.DisplayEmployees(staffChoice);
                    break;

                case "2":
                    Message.ShowStudentSortingMenu();
                    string studentChoice = Console.ReadLine();

                    _studentUIService.DisplayStudents(studentChoice);
                    break;

                case "3":
                    Message.ShowClassSelectionMenu();
                    string classChoice = Console.ReadLine();     // 1. Get the class selection from user (classChoice)       
                    Message.ShowClassSortingMenu();                     
                    string sortChoice = Console.ReadLine() ?? ""; // 2. Get their sorting preference (sortChoice)

                    // 3. Pass parameters to DisplayClassStudents to display students from the chosen class in their preferred sort order
                    _studentUIService.DisplayClassStudents(classChoice, sortChoice); 
                    break;

                case "4":
                    // Display recent grade information
                    _gradeUIService.DisplayRecentGrades();
                    break;

                case "5":
                    // Display course statistics and performance metrics
                    _gradeUIService.DisplayCourseStatistics();
                    break;

                case "6":
                    // Handle student management operations (add/edit/delete)
                    Message.ShowEditStudentMenu();
                    string editStudentChoice = Console.ReadLine();

                    _studentUIService.HandleStudentMenu(editStudentChoice);
                    break;

                case "7":
                    // Handle employee management operations (add/edit/delete)
                    Message.ShowEditEmployeesMenu();
                    string editEmployeeChoice = Console.ReadLine();

                    _employeeUIService.HandleEmployeeMenu(editEmployeeChoice);
                    break;

                case "0":
                    // Exit application
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    break;
            }
            if (running)
            {   // This function will give user time to reade data from database before clear and continue to main menu
                Message.promtUser();
            }
        }
    }
}