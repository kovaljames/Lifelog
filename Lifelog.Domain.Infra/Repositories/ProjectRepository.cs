using Lifelog.Domain.Entities;
using Lifelog.Domain.Infra.Contexts;
using Lifelog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lifelog.Domain.Infra.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }
    
    public async void Create(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public Project GetById(int id, string user)
    {
        return _context.Projects
            .AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Project> GetAll(string user, int page = 0, int pageSize = 25)
    {
        return _context.Projects
            .AsNoTracking()
            .Skip(page * pageSize)
            .Take(pageSize);
    }

    public IEnumerable<Project> GetAllActive(string user, int page = 0, int pageSize = 25)
    {
        return _context.Projects
            .AsNoTracking()
            .Where(x => x.Active == true)
            .Skip(page * pageSize)
            .Take(pageSize);
    }

    public IEnumerable<Project> GetAllInactive(string user, int page = 0, int pageSize = 25)
    {
        return _context.Projects
            .AsNoTracking()
            .Where(x => x.Active == false)
            .Skip(page * pageSize)
            .Take(pageSize);
    }

    public async void Update(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async void Delete(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}