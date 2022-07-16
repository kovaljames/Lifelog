using Flunt.Notifications;
using Flunt.Validations;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Commands;

public class CreateUserCommand : Notifiable<Notification>, ICommand
{
    public CreateUserCommand(string email, string password)
    {
        Name = email.Substring(0, email.IndexOf("@"));
        Email = email;
        Password = password;
        Slug = email.Replace("@", ".").Replace(".", "-");
        Joined = DateTime.Now;
        RefUser = "";
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Slug { get; set; }
    public DateTime Joined { get; set; }
    public string RefUser { get; set; }

    public void Validate()
    {
        AddNotifications(new Contract<User>()
            .Requires()
            .IsGreaterThan(Name, 5, "Title", "Mínimo de 5 caracteres!")
            .IsEmail(Email, "Email", "E-mail inválido!")
            .IsGreaterThan(Password, 10, "Password", "Pelo menos 10 caracteres!")
        );
    }
}