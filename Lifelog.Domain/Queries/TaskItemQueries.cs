using Lifelog.Domain.Entities;
using System.Linq.Expressions;

namespace Lifelog.Domain.Queries;

public static class TaskItemQueries
{
    public static Expression<Func<TaskItem, bool>> GetAll(int userId)
    {
        return x => x.User.Id == userId;
    }

    public static Expression<Func<TaskItem, bool>> GetAllDone(int userId)
    {
        return x => x.User.Id == userId && x.Done;
    }

    public static Expression<Func<TaskItem, bool>> GetAllUndone(int userId)
    {
        return x => x.User.Id == userId && x.Done == false;
    }

    public static Expression<Func<TaskItem, bool>> GetById(int id, int userId)
    {
        return x => x.Id == id && x.User.Id == userId;
    }
    
    public static Expression<Func<TaskItem, bool>>GetByPeriod(
        int userId,
        DateTime date,
        bool done)
    {
        return x =>
            x.User.Id == userId &&
            x.Done == done &&
            x.DateEnd.Date == date.Date;
    }
}