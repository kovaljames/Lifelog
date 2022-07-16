using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Repositories;

public interface ITaskItemRepository
{
    void Create(TaskItem task);
    TaskItem GetById(int id, string user);
    IEnumerable<TaskItem> GetAll(string user, int page = 0, int pageSize = 25);
    IEnumerable<TaskItem> GetAllByTitle(string user, string title,
        int page = 0, int pageSize = 25);
    IEnumerable<TaskItem> GetAllDone(string user, int page = 0, int pageSize = 25);
    IEnumerable<TaskItem> GetAllUndone(string user, int page = 0, int pageSize = 25);
    IEnumerable<TaskItem> GetByProject(int projectId, int page = 0, int pageSize = 25);
    IEnumerable<TaskItem> GetByPeriod(
        string user, DateTime date, bool done, int page = 0, int pageSize = 25);
    void Update(TaskItem task);
    void Delete(TaskItem task);
}