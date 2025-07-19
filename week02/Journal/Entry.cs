using System;

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;
    public string _mood;

    public Entry(string prompt, string response, string mood)
    {
        _date = DateTime.Now.ToString("yyyy-MM-dd");
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    public string GetDisplayText()
    {
        return $"Date: {_date}\nMood: {_mood}\nPrompt: {_prompt}\nResponse: {_response}\n";
    }

    // Save in a format that's CSV-friendly (quoted strings)
    public string GetSaveText()
    {
        return $"\"{_date}\",\"{_mood}\",\"{_prompt}\",\"{_response}\"";
    }

    public static Entry FromSavedText(string line)
    {
        // Remove leading/trailing quotes and split by ","
        string[] parts = line.Split("\",\"");

        // Clean up any remaining quote characters
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i] = parts[i].Replace("\"", "").Trim();
        }

        Entry entry = new Entry(parts[2], parts[3], parts[1]);
        entry._date = parts[0];
        return entry;
    }
}
