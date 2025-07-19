using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddEntry()
    {
        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        Console.Write("How do you feel? (e.g., Happy, Sad, Grateful, Anxious): ");
        string mood = Console.ReadLine();

        Entry entry = new Entry(prompt, response, mood);
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            Console.WriteLine(entry.GetDisplayText());
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.GetSaveText());
            }
        }

        Console.WriteLine($"Journal saved to {filename}");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    Entry entry = Entry.FromSavedText(line);
                    _entries.Add(entry);
                }
            }

            Console.WriteLine($"Journal loaded from {filename}");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    // Optional: Show a count of entries by mood
    public void ShowMoodStats()
    {
        Console.WriteLine("\nMood Summary:");
        Dictionary<string, int> moodCount = new Dictionary<string, int>();
        foreach (Entry entry in _entries)
        {
            if (moodCount.ContainsKey(entry._mood))
                moodCount[entry._mood]++;
            else
                moodCount[entry._mood] = 1;
        }

        foreach (var pair in moodCount)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value} entries");
        }
        Console.WriteLine();
    }
}
