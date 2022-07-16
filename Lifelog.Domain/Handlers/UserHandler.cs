using Flunt.Notifications;
using Lifelog.Domain.Commands;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;
using Lifelog.Domain.Handlers.Contracts;
using Lifelog.Domain.Repositories;

namespace Lifelog.Domain.Handlers;

public class UserHandler :
    Notifiable<Notification>,
    IHandler<LoginCommand>,
    IHandler<CreateUserCommand>,
    IHandler<UpdateUserCommand>
{
    private readonly IUserRepository _repository;
    
    public UserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(LoginCommand command)
    {
        // Login
        var user = _repository.GetByEmail(command.Email);
        
        return new GenericCommandResult(
            true, "Login bem sucedido!", user);
    }
    
    public ICommandResult Handle(CreateUserCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Create user
        var user = new User(command.Email, command.Password);

        // Save task on database
        _repository.Create(user);

        // Notify user
        return new GenericCommandResult(
            true, "Usuário criado!", user);
    }
    
    public ICommandResult Handle(UpdateUserCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Get user
        var user = _repository.GetById(command.Id);

        user.UpdateName(command.Name);
        user.UpdateEmail(command.Email);
        user.UpdatePassword(command.Password);

        // Update user on database
        _repository.Update(user);

        // Notify user
        return new GenericCommandResult(
            true, "Usuário criado!", user);
    }

    public ICommandResult Handle(int id)
    {
        // Get user
        var user = _repository.GetById(id);
        
        // Delete user
        _repository.Delete(user);
        
        // Notify user
        return new GenericCommandResult(
            true, "Usuário deletado!", null);
    }
}