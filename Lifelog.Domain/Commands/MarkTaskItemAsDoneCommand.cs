using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class MarkTaskItemAsDoneCommand : Notifiable<Notification>, ICommand
{
    public MarkTaskItemAsDoneCommand() { }

    public MarkTaskItemAsDoneCommand(int id, string user, DateTime dateEnd)
    {
        Id = id;
        User = user;
        DateEnd = dateEnd;
    }

    public int Id { get; set; }
    public string User { get; set; }
    public DateTime DateEnd { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<TaskItem>()
                .Requires()
        );
    }
}