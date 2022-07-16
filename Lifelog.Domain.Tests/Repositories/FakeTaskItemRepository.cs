using Lifelog.Domain.Entities;
using Lifelog.Domain.Repositories;

namespace Lifelog.Domain.Tests.Repositories;

public class FakeTaskItemRepository : ITaskItemRepository
{
    public void Create(TaskItem task)
    {
        throw new NotImplementedException();
    }

    public TaskItem GetById(int id, string user)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetAll(string user, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetAllByTitle(string user, string title, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetAllDone(string user, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetAllUndone(string user, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetByProject(int projectId, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskItem> GetByPeriod(string user, DateTime date, bool done, int page = 0, int pageSize = 25)
    {
        throw new NotImplementedException();
    }

    public void Update(TaskItem task)
    {
        throw new NotImplementedException();
    }

    public void Delete(TaskItem task)
    {
        throw new NotImplementedException();
    }
}