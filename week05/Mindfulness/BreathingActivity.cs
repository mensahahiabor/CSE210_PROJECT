using System;
using System.Threading;

public class BreathingActivity : MindfulnessActivity
{
    protected override string GetDescription()
    {
        return "This activity helps you relax by guiding you through slow breathing.";
    }

    protected override void RunActivity()
    {
        DateTime end = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < end)
        {
            Console.Write("Breathe in...");
            Countdown(3);
            Console.WriteLine();

            Console.Write("Hold...");
            Countdown(2);
            Console.WriteLine();

            Console.Write("Breathe out...");
            Countdown(3);
            Console.WriteLine();
        }
    }
}
