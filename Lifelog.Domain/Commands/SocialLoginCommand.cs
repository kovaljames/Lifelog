using Lifelog.Domain.Commands.Contracts;

namespace Lifelog.Domain.Commands;

public class SocialLoginCommand : ICommand
{
    public SocialLoginCommand(string refUser)
    {
        RefUser = refUser;
    }
    
    public string RefUser { get; set; }
        
    public void Validate()
    {
        throw new NotImplementedException();
    }
}