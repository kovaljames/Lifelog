using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;

namespace Lifelog.Domain.Commands;

public class UpdateTaskItemCommand : Notifiable<Notification>, ICommand
{
    public UpdateTaskItemCommand() { }
    
    public UpdateTaskItemCommand(int id, string title, DateTime dateInit,
        DateTime dateEnd, string user, int projectId = 0, string desc = "")
    {
        Id = id;
        Title = title;
        DateInit = dateInit;
        DateEnd = dateEnd;
        Desc = desc;
        User = user;
        ProjectId = projectId;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime DateInit { get; set; }
    public DateTime DateEnd { get; set; }
    public string Desc { get; set; }
    public string User { get; set; }
    public int ProjectId { get; set; }
 
    public void Validate()
    {
        AddNotifications(new Contract<Task>()
            .Requires()
            .IsGreaterThan(Title, 2, "Title", "Mínimo 3 caracteres!")
            .IsLowerThan(Title, 161, "Title", "Máximo 160 catacteres!")
            .IsGreaterThan(User, 5, "User", "Usuário inválido!")
        );
    }
}
