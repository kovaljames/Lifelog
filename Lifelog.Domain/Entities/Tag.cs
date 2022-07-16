namespace Lifelog.Domain.Entities;

public class Tags
{
    public Tags(string title, string color)
    {
        Title = title;
        Color = color;
    }

    public string Title { get; private set; }
    public string Color { get; private set; }
}