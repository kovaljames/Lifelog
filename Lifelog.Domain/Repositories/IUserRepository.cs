using Lifelog.Domain.Entities;

namespace Lifelog.Domain.Repositories;

public interface IUserRepository
{
    void Create(User user);
    User GetById(int id);
    User GetByEmail(string email);
    IEnumerable<User> GetAll(int page = 0, int pageSize = 25);
    IEnumerable<User> GetAllByProject(int projectId, int page = 0, int pageSize = 25);
    void Update(User user);
    void Delete(User user);
}