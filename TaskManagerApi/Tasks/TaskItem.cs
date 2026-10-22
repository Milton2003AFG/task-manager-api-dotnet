using TaskManagerApi.Projects;

namespace TaskManagerApi.Tasks;

public class TaskItem
{
    public long Id {get; private set;}
    public required string Title {get; set;}
    public string? Description {get; set;}
    public TaskItemStatus Status {get; set;} =  TaskItemStatus.Todo;
    public DateTime? DueDate {get; set;}
    public required long ProjectId {get; init;}
    public Project? Project {get; set;}
}