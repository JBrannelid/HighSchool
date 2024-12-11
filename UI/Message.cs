namespace HighSchool.UI
{
    public static class Message
    {
        public static void DisplayMenueChoice()
        {
            Console.Clear();
            Console.WriteLine("=== Skolsystem ===");
            Console.WriteLine("1. Visa personal");
            Console.WriteLine("2. Visa alla elever");
            Console.WriteLine("3. Visa elever i en klass");
            Console.WriteLine("4. Visa senaste månadens betyg");
            Console.WriteLine("5. Visa kursstatistik");
            Console.WriteLine("6. Elevinställningar");
            Console.WriteLine("7. Personalinställningar");
            Console.WriteLine("0. Avsluta");
        }

        public static void ShowEmployeesMenu()
        {
            Console.WriteLine("\nVälj kategori:");
            Console.WriteLine("1. Alla anställda");
            Console.WriteLine("2. Bara lärare");
            Console.WriteLine("3. Bara administratörer");
            Console.WriteLine("4. Bara rektorer");
        }

        public static void ShowStudentSortingMenu()
        {
            Console.WriteLine("\nVälj sortering:");
            Console.WriteLine("1. Förnamn (A-Ö)");
            Console.WriteLine("2. Förnamn (Ö-A)");
            Console.WriteLine("3. Efternamn (A-Ö)");
            Console.WriteLine("4. Efternamn (Ö-A)");
        }

        // In Message.cs
        public static void ShowClassSortingMenu()
        {
            Console.WriteLine("\nVälj sortering:");
            Console.WriteLine("1. Förnamn (A-Ö)");
            Console.WriteLine("2. Förnamn (Ö-A)");
            Console.WriteLine("3. Efternamn (A-Ö)");
            Console.WriteLine("4. Efternamn (Ö-A)");
        }

        public static void ShowClassSelectionMenu()
        {
            Console.WriteLine("\nÅrkull 1");
            Console.WriteLine("1. 1A");
            Console.WriteLine("2. 1B");
            Console.WriteLine("3  1C\n");
            Console.WriteLine("\nÅrkull 2");
            Console.WriteLine("4. 2A");
            Console.WriteLine("5. 2B");
            Console.WriteLine("6. 2C\n");
            Console.WriteLine("\nÅrkull 3");
            Console.WriteLine("7  3A");
            Console.WriteLine("8. 3B");
            Console.WriteLine("9. 3C");
        }

        public static void ShowEditStudentMenu()
        {
            Console.WriteLine("\nVad vill du göra med elevvyn?");
            Console.WriteLine("1. Lägg till ny elev");
            Console.WriteLine("2. Redigera en befintlig elev");
            Console.WriteLine("3. Ta bort en elev från skolan");
        }

        public static void ShowEditEmployeesMenu()
        {
            Console.WriteLine("\nVad vill du göra med personalvyn?");
            Console.WriteLine("1. Lägg till en ny personal");
            Console.WriteLine("2. Redigera en befintlig personal");
            Console.WriteLine("3. Ta bort en personal från skolan");
        }

        public static void promtUser()
        {
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ShowPositionsMenu()
        {
            Console.WriteLine("\nTillgängliga positioner:");
            Console.WriteLine("1. (100) Lärare");
            Console.WriteLine("2. (101) Rektor");
            Console.WriteLine("3. (102) Administratör");
        }
    }
}