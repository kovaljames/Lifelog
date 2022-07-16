using Lifelog.Domain.Entities;
using Lifelog.Domain.Infra.Contexts;
using Lifelog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lifelog.Domain.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async void Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public User GetById(int id)
    {
        return _context
            .Users
            .Include(x => x.Role)
            .FirstOrDefault(x => x.Id == id);
    }

    public User GetByEmail(string email)
    {
        return _context
            .Users
            .Include(x => x.Role)
            .FirstOrDefault(x => x.Email == email);
    }

    public IEnumerable<User> GetAll(int page = 0, int pageSize = 25)
    {
        return _context.Users
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.Joined)
            .ToList();
    }

    public IEnumerable<User> GetAllByProject(int projectId, int page = 0, int pageSize = 25)
    {
        return _context.Users
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize)
            .OrderBy(x => x.Joined)
            .ToList();
    }

    public async void Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async void Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}