namespace Lifelog.Domain.Entities;

public class Project
{
    public Project(string name, string? color, int? status, DateTime createdAt, DateTime? dueDate, 
        string user, bool isPublic, bool active)
    {
        Name = name;
        Slug = new Guid();
        Status = status;
        Color = color;
        CreatedAt = createdAt;
        DueDate = dueDate;
        User = user;
        IsPublic = isPublic;
        Active = active;
        Tasks = new List<TaskItem>();
        Users = new List<User>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public Guid Slug { get; private set; }
    public string? Color { get; private set; }
    public int? Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public string User { get; private set; }
    public bool IsPublic { get; private set; }
    public bool Active { get; private set; }
    public Workspace Workspace { get; private set; }
    public IList<TaskItem> Tasks { get; private set; }
    public IList<User> Users { get; private set; }
    
    public void UpdateName(string name) => Name = name;
    public void UpdateColor(string? color) => Color = color;
    public void UpdateStatus(int? status) => Status = status;
    public void UpdateDueDate(DateTime? dueDate) => DueDate = dueDate;
    public void UpdateUser(string user) => User = user;
    public void UpdateIsPublic(bool isPublic) => IsPublic = isPublic;
    public void UpdateActive(bool active) => Active = active;
}