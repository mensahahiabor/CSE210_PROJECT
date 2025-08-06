using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => null
            };

            if (activity == null)
            {
                if (choice == "4")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                Console.WriteLine("Invalid choice. Try again.");
                continue;
            }

            activity.Start();
            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }
    }
}
