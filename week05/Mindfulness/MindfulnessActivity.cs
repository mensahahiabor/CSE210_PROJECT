using System;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected int duration;

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {GetType().Name}!");
        Console.WriteLine(GetDescription());

        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Get ready...");
        Spinner(3);

        RunActivity();

        Console.WriteLine("\nWell done!");
        Spinner(3);
    }

    protected abstract string GetDescription();
    protected abstract void RunActivity();

    protected void Spinner(int seconds)
    {
        string[] symbols = { "/", "-", "\\", "|" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write(symbols[i % symbols.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
            i++;
        }
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }
}

