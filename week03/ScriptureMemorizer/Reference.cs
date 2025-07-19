class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string reference)
    {
        string[] parts = reference.Split(' ');
        Book = string.Join(" ", parts.Take(parts.Length - 1));

        string[] verseParts = parts.Last().Split(':');
        Chapter = int.Parse(verseParts[0]);

        string[] verses = verseParts[1].Split('-');
        StartVerse = int.Parse(verses[0]);
        EndVerse = verses.Length > 1 ? int.Parse(verses[1]) : (int?)null;
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}" : $"{Book} {Chapter}:{StartVerse}";
    }
}