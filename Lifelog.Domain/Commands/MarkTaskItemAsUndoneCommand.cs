using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class MarkTaskItemAsUndoneCommand : Notifiable<Notification>, ICommand
{
    public MarkTaskItemAsUndoneCommand() { }

    public MarkTaskItemAsUndoneCommand(int id, string user)
    {
        Id = id;
        User = user;
    }

    public int Id { get; set; }
    public string User { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<TaskItem>()
                .Requires()
        );
    }
}