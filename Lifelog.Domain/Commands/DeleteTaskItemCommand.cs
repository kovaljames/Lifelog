using Flunt.Notifications;
using Lifelog.Domain.Commands.Contracts;

namespace Lifelog.Domain.Commands;

public class DeleteTaskItemCommand : Notifiable<Notification>, ICommand
{
    public DeleteTaskItemCommand() { }
    
    public DeleteTaskItemCommand(int id, string user)
    {
        Id = id;
        User = user;
    }
    
    public int Id { get; set; }
    public string User { get; set; }

    public void Validate()
    {
        throw new NotImplementedException();
    }
}