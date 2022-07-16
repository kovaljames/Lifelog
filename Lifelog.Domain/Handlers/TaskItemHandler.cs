using Flunt.Notifications;
using Lifelog.Domain.Commands;
using Lifelog.Domain.Commands.Contracts;
using Lifelog.Domain.Entities;
using Lifelog.Domain.Handlers.Contracts;
using Lifelog.Domain.Repositories;

namespace Lifelog.Domain.Handlers;

public class TaskItemHandler :
    Notifiable<Notification>,
    IHandler<CreateTaskItemCommand>,
    IHandler<UpdateTaskItemCommand>,
    IHandler<MarkTaskItemAsDoneCommand>,
    IHandler<MarkTaskItemAsUndoneCommand>
{
    private readonly ITaskItemRepository _repository;
    public TaskItemHandler(ITaskItemRepository repository)
    {
        _repository = repository;
    }

    public ICommandResult Handle(CreateTaskItemCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Create a task
        var task = new TaskItem(command.Title, command.DateInit, command.DateEnd,
             command.User, command.ProjectId, command.UserId, command.Desc);

        // Save task on database
        _repository.Create(task);

        // Notify user
        return new GenericCommandResult(
            true, "Tarefa Salva", task);
    }

    public ICommandResult Handle(UpdateTaskItemCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Recover task (rehidration)
        var task = _repository.GetById(command.Id, command.User);

        // Update title
        task.UpdateTitle(command.Title);
        task.UpdateDateEnd(command.DateEnd);

        // Save task on database
        _repository.Update(task);

        // Notify user
        return new GenericCommandResult(
            true, "Tarefa Editada!", task);
    }

    public ICommandResult Handle(MarkTaskItemAsDoneCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Recover task (rehidration)
        var task = _repository.GetById(command.Id, command.User);
        
        task.MarkAsDone(command.DateEnd);

        // Save task on database
        _repository.Update(task);

        // Notify user
        return new GenericCommandResult(
            true, "Tarefa Editada!", task);
    }

    public ICommandResult Handle(MarkTaskItemAsUndoneCommand command)
    {
        // Fail fast validations
        command.Validate();
        if (!command.IsValid)
            return new GenericCommandResult(
                false,
                "Ops, algo deu errado!",
                command.Notifications);

        // Recover task
        var task = _repository.GetById(command.Id, command.User);
        
        task.MarkAsUndone();

        // Save task on database
        _repository.Update(task);

        // Notify user
        return new GenericCommandResult(
            true, "Tarefa Editada!", task);
    }

    public ICommandResult Handle(DeleteTaskItemCommand command)
    {
        // Recover task
        var task = _repository.GetById(command.Id, command.User);
        
        // Delete task
        _repository.Delete(task);
        
        // Notify user
        return new GenericCommandResult(
            true, "Tarefa Deletada!", task);
    }
}