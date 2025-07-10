using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nPlease choose one of the following options:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Show mood summary");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    Console.Write("Enter filename to save (e.g., journal.csv): ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    journal.ShowMoodStats();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}

/*
EXCEEDS CORE REQUIREMENTS:
- Added mood tracking to journal entries
- Entries saved in CSV-compatible format with proper quoting
- Mood summary (entry count per mood) shown in option 5
- Uses abstraction with Entry and Journal classes in separate files
*/
