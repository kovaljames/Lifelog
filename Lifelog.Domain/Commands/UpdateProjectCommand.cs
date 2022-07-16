using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class UpdateProjectCommand : Notifiable<Notification>, ICommand
{
    public UpdateProjectCommand(int id, string name, Guid slug, string? color, DateTime? dueDate,
        int? status, bool active, bool isPublic, string user, int workspace)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Color = color;
        DueDate = dueDate;
        Status = status;
        Active = active;
        IsPublic = isPublic;
        User = user;
        Workspace = workspace;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public Guid Slug { get; set; }
    public string? Color { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Status { get; set; }
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