using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static bool timeUp = false;
    static DateTime startTime;

    static void Main()
    {
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");
        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures found. Please check the scriptures file.");
            return;
        }

        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        Console.WriteLine("Choose difficulty level: (1) Easy (2) Medium (3) Hard");
        int difficulty = GetUserChoice(1, 3);
        int wordsToHide = difficulty == 1 ? 1 : difficulty == 2 ? 3 : 5;

        Console.WriteLine("Would you like to enable timed mode? (y/n)");
        bool timedMode = Console.ReadLine().Trim().ToLower() == "y";

        int timerSeconds = 30;
        if (timedMode)
        {
            Console.WriteLine("Enter the number of seconds for the timer:");
            timerSeconds = GetUserChoice(10, 120);
        }

        if (timedMode)
        {
            Task.Run(() => StartTimer(timerSeconds));
        }

        while (!scripture.IsCompletelyHidden() && !timeUp)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (timedMode)
            {
                Console.WriteLine($"\n⏳ Time Left: {timerSeconds - (int)(DateTime.Now - startTime).TotalSeconds} seconds");
            }

            Console.WriteLine("\nOptions: Press Enter to hide words | Type 'hint' for a hint | Type 'quit' to exit");

            if (timeUp) break;

            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit") return;
            else if (input == "hint") scripture.RevealOneWord();
            else scripture.HideRandomWords(wordsToHide);
        }

        Console.Clear();
        if (timeUp)
        {
            Console.WriteLine(" Time's up! Better luck next time.");
            Console.WriteLine($"Scripture: {scripture.GetOriginalText()}");
        }
        else
        {
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine("\n Memorization complete!");
        }
    }

    static void StartTimer(int seconds)
    {
        startTime = DateTime.Now;
        while ((int)(DateTime.Now - startTime).TotalSeconds < seconds)
        {
            Thread.Sleep(1000);
        }
        timeUp = true;
    }

    static List<Scripture> LoadScriptures(string filePath)
    {
        List<Scripture> scriptures = new List<Scripture>();

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines("scriptures.txt");
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        Reference reference = new Reference(parts[0]);
                        scriptures.Add(new Scripture(reference, parts[1]));
                    }
                }
            }
        }
        return scriptures;
    }

    static int GetUserChoice(int min, int max)
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
        {
            Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }
        return choice;
    }
}