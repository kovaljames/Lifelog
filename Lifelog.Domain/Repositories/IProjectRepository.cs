using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Repositories;

public interface IProjectRepository
{
    void Create(Project project);
    Project GetById(int id, string user);
    IEnumerable<Project> GetAll(string user, int page = 0, int pageSize = 25);
    IEnumerable<Project> GetAllActive(string user, int page = 0, int pageSize = 25);
    IEnumerable<Project> GetAllInactive(string user, int page = 0, int pageSize = 25);
    void Update(Project project);
    void Delete(Project project);
}