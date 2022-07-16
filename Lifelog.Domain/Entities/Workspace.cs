namespace Lifelog.Domain.Entities;

public class Workspace
{
    public Workspace(string name)
    {
        Name = name;
        Projects = new List<Project>();
        Members = new List<User>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public IList<Project> Projects { get; private set; }
    public IList<User> Members { get; private set; }
}