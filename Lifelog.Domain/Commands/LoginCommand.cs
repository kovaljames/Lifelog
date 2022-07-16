using Lifelog.Domain.Commands.Contracts;

namespace Lifelog.Domain.Commands;

public class LoginCommand : ICommand
{
    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public void Validate()
    {
        throw new NotImplementedException();
    }
}