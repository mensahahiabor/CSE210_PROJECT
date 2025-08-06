using System;
using System.Collections.Generic;

public class ReflectionActivity : MindfulnessActivity
{
    private static readonly List<string> Prompts = new()
    {
        "Think of a time you were really brave.",
        "Recall a moment you helped someone in need.",
        "Remember a time when you achieved something great."
    };

    private static readonly List<string> Questions = new()
    {
        "Why was this experience meaningful?",
        "What did you learn from it?",
        "How did you feel after it?",
        "What made this experience stand out?"
    };

    protected override string GetDescription()
    {
        return "This activity helps you reflect on meaningful experiences.";
    }

    protected override void RunActivity()
    {
        Random rnd = new Random();
        Console.WriteLine("Consider this prompt:");
        Console.WriteLine(Prompts[rnd.Next(Prompts.Count)]);
        Spinner(3);

        Console.WriteLine("\nThink deeply about each of the following questions:");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string question = Questions[rnd.Next(Questions.Count)];
            Console.WriteLine($"\n> {question}");
            Spinner(4); // This replaces PauseWithAnimation()
        }
    }
}
