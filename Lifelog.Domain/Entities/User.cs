using System.Text.Json.Serialization;

namespace Lifelog.Domain.Entities;

public class User
{
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
    public User(string name, string email, string password,
        string image, string slug)
    {
        Name = name;
        Email = email;
        Password = password;
        Image = image;
        Slug = slug;
        Auth2Fa = false;
        Language = 1;
        Timezone = 1;
        Tasks = new List<TaskItem>();
        Projects = new List<Project>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    [JsonIgnore]
    public string Password { get; private set; }
    public string Image { get; private set; }
    public string Slug { get; private set; }
    public bool Auth2Fa { get; private set; }
    public int Language { get; private set; }
    public int Timezone { get; private set; }
    public DateTime Joined { get; private set; }
    public Role Role { get; private set; }
    public IList<TaskItem> Tasks { get; private set; }
    public IList<Project> Projects { get; private set; }

    public void UpdateName(string name) => Name = name;
    public void UpdateEmail(string email) => Email = email;
    public void UpdatePassword(string password) => Password = password;
}