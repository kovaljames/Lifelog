namespace Lifelog.Domain.Entities;

public class TaskItem
{
    public TaskItem(string title, DateTime dateInit, DateTime dateEnd,
        string userSlug, int? projectId, int? userId, string desc = "")
    {
        Title = title;
        DateInit = dateInit;
        DateEnd = dateEnd;
        Desc = desc;
        Done = false;
        isPublic = false;
        UserSlug = userSlug;
        UserId = userId;
        ProjectId = projectId;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public DateTime DateInit { get; private set; }
    public DateTime DateEnd { get; private set; }
    public string Desc { get; private set; }
    public bool Done { get; private set; }
    public bool isPublic { get; private set; }
    public int? ProjectId { get; private set; }
    public string UserSlug { get; private set; }
    public int? UserId { get; private set; }
    public User? User { get; private set; }
    public Project? Project { get; private set; }

    public void MarkAsDone(DateTime dateEnd)
    {
        Done = true;
        DateEnd = dateEnd;
    }
    public void MarkAsUndone() => Done = false;
    public void UpdateTitle(string title) => Title = title;
    public void UpdateDateInit(DateTime dateInit) => DateInit = dateInit;
    public void UpdateDateEnd(DateTime dateEnd) => DateEnd = dateEnd;
}