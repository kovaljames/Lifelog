using Flunt.Notifications;
using Lifelog.Domain.Commands;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;
using Lifelog.Domain.Handlers.Contracts;
using Lifelog.Domain.Repositories;

namespace Lifelog.Domain.Handlers;

public class ProjectHandler :
    Notifiable<Notification>,
    IHandler<CreateProjectCommand>,
    IHandler<UpdateProjectCommand>,
    IHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _repository;

    public ProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateProjectCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        var project = new Project(command.Name, null, null, command.CreatedAt, null,
            command.User, command.IsPublic, command.Active);
        
        _repository.Create(project);

        return new GenericCommandResult(true, "Projeto criado!", project);
    }
    
    public ICommandResult Handle(UpdateProjectCommand command)
    {
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);
        
        var project = _repository.GetById(command.Id, command.User);

        project.UpdateName(command.Name);
        project.UpdateColor(command.Color);
        project.UpdateStatus(command.Status);
        project.UpdateDueDate(command.DueDate);
        project.UpdateActive(command.Active);
        project.UpdateIsPublic(command.IsPublic);
        project.UpdateUser(command.User);
        
        _repository.Update(project);

        return new GenericCommandResult(true, "Projeto editado!", project);
    }
    
    public ICommandResult Handle(DeleteProjectCommand command)
    {
        var project = _repository.GetById(command.Id, command.User);
        
        _repository.Delete(project);
        
        // Notify user
        return new GenericCommandResult(
            true, "Projeto Deletado!", project);
    }
}