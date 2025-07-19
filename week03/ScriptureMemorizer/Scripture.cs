using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords(int count)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden).ToList();
        if (visibleWords.Count == 0) return;

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            Word wordToHide = visibleWords[_random.Next(visibleWords.Count)];
            wordToHide.Hide();
            visibleWords.Remove(wordToHide);
        }
    }

    public void RevealOneWord()
    {
        List<Word> hiddenWords = _words.Where(w => w.IsHidden).ToList();
        if (hiddenWords.Count > 0)
        {
            Word wordToReveal = hiddenWords[_random.Next(hiddenWords.Count)];
            wordToReveal.Reveal();
            Console.WriteLine($"🔍 Hint: {wordToReveal.Text}");
            Thread.Sleep(1500);
        }
        else
        {
            Console.WriteLine("All words are already visible!");
        }
    }

    public string GetDisplayText()
    {
        return $"{_reference}\n" + string.Join(" ", _words.Select(w => w.GetDisplayText()));
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    public string GetOriginalText()
    {
        return $"{_reference}\n" + string.Join(" ", _words.Select(w => w.Text));
    }
}