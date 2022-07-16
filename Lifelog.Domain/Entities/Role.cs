namespace Lifelog.Domain.Entities;

public class Role
{
    public Role()
    {
        Users = new List<User>();
    }

    public int Id { get; private set; }
    public IList<User> Users { get; private set; }
}