using TaskManagerApi.Users;

namespace TaskManagerApi.Projects;

public class Project
{
    public long Id {get; private set;}
    public required string Name {get; set;}
    public required long OwnerId {get; init;}
    public User? Owner {get; set; }
}