using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Creating video objects
        Video video1 = new Video("Introduction to C#", "CodeAcademy", 600);
        Video video2 = new Video("Object-Oriented Programming", "TechWithTim", 1200);
        Video video3 = new Video("Abstraction in C#", "ProgrammingSimplified", 900);

        // Adding comments to video1
        video1.AddComment(new Comment("Alice", "Great explanation!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "This was exactly what I needed."));

        // Adding comments to video2
        video2.AddComment(new Comment("David", "OOP concepts made simple."));
        video2.AddComment(new Comment("Eva", "Nice breakdown of the principles."));
        video2.AddComment(new Comment("Frank", "This really helped me understand abstraction."));

        // Adding comments to video3
        video3.AddComment(new Comment("Grace", "Well structured tutorial."));
        video3.AddComment(new Comment("Hannah", "I wish all tutorials were like this."));
        video3.AddComment(new Comment("Isaac", "Very informative and easy to follow."));

        // Storing videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Displaying video details
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine($"  - {comment.Commenter}: \"{comment.Text}\"");
            }

            Console.WriteLine();
        }
    }
}

// Video class
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }
}

// Comment class
class Comment
{
    public string Commenter { get; set; }
    public string Text { get; set; }

    public Comment(string commenter, string text)
    {
        Commenter = commenter;
        Text = text;
    }
}