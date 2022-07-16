using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class CreateProjectCommand : Notifiable<Notification>, ICommand
{
    public CreateProjectCommand(string name, DateTime createdAt,
        bool isPublic, string user, int workspace)
    {
        Name = name;
        Slug = new Guid();
        CreatedAt = createdAt;
        Active = true;
        IsPublic = isPublic;
        User = user;
        Workspace = workspace;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Guid Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
    public bool IsPublic { get; set; }
    public string User { get; set; }
    public int Workspace { get; set; }
    
    public void Validate()
    {
        AddNotifications(new Contract<TaskItem>()
            .Requires()
            .IsGreaterThan(Name, 3, "Title", "Mínimo de 3 caracteres!")
        );
    }
}
