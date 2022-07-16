using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class UpdateUserCommand : Notifiable<Notification>, ICommand
{
    public UpdateUserCommand(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<User>()
            .Requires()
            .IsGreaterThan(Name, 5, "Name", "Mínimo de 5 caracteres!")
            .IsEmail(Email, "Email", "E-mail inválido!")
            .IsGreaterThan(Password, 10, "Password", "Pelo menos 10 caracteres!")
        );
    }
}