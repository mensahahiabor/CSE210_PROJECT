using System;
using System.Collections.Generic;

public class ListingActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new()
    {
        "List things you're grateful for:",
        "List people who make you smile:",
        "List positive things that happened today:"
    };

    protected override string GetDescription()
    {
        return "This activity encourages you to list positive things in your life.";
    }

    protected override void RunActivity()
    {
        Random rnd = new Random();
        Console.WriteLine(Prompts[rnd.Next(Prompts.Count)]);
        Spinner(3);

        List<string> items = new();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                items.Add(input);
        }

        Console.WriteLine($"\nYou listed {items.Count} items!");
    }
}
