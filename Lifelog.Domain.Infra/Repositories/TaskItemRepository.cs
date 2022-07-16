using Lifelog.Domain.Entities;
using Lifelog.Domain.Infra.Contexts;
using Lifelog.Domain.Queries;
using Lifelog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lifelog.Domain.Infra.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly DataContext _context;

    public TaskItemRepository(DataContext context)
    {
        _context = context;
    }

    public void Create(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public IEnumerable<TaskItem> GetAll(string user, int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x => x.UserSlug == user)
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.DateInit);
    }

    public IEnumerable<TaskItem> GetAllByTitle(string user, string title,
        int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x => x.UserSlug == user && x.Title.StartsWith(title))
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderByDescending(x => x.DateInit);
    }

    public IEnumerable<TaskItem> GetAllDone(string user, int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x => x.UserSlug == user && x.Done == true)
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.DateEnd);
    }

    public IEnumerable<TaskItem> GetAllUndone(string user, int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x => x.UserSlug == user && x.Done == false)
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.DateEnd);
    }

    public IEnumerable<TaskItem> GetByProject(int projectId, int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.DateEnd);
    }

    public IEnumerable<TaskItem> GetByPeriod(string user, DateTime date, bool done,
        int page = 0, int pageSize = 25)
    {
        return _context.Tasks
            .AsNoTracking()
            .Where(x =>
                x.UserSlug == user &&
                x.Done == done &&
                x.DateInit.Date == date.Date)
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.DateEnd);
    }

    public TaskItem GetById(int id, string user)
    {
        return _context.Tasks
            .FirstOrDefault(x => x.UserSlug == user && x.Id == id);
    }

    public void Update(TaskItem task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }
    
    public void Delete(TaskItem task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }
}