using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class CreateTaskItemCommand : Notifiable<Notification>, ICommand
{
    public CreateTaskItemCommand() { }

    public CreateTaskItemCommand(string title, DateTime dateInit, DateTime dateEnd,
        string user, int? projectId, int? userId, string desc = "")
    {
        Title = title;    
        DateInit = dateInit;
        DateEnd = dateEnd;
        Desc = desc;
        User = user;
        ProjectId = projectId;
        UserId = userId;
    }
    
    public string Title { get; set; }
    public DateTime DateInit { get; set; }
    public DateTime DateEnd { get; set; }
    public string Desc { get; set; }
    public string User { get; set; }
    public int? ProjectId { get; set; }
    public int? UserId { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<TaskItem>()
            .Requires()
            .IsGreaterThan(Title, 2, "Title", "Mínimo 3 caracteres!")
            .IsLowerThan(Title, 161, "Title", "Máximo 160 catacteres!")
            .IsGreaterThan(User, 5, "User", "Usuário inválido!")
        );
    }
}