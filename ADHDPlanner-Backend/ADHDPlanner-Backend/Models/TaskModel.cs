namespace ADHDPlanner_Backend.Models
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public List<TaskModel> Subtasks { get; set; }

        public TaskModel(int id, string name, bool isComplete, int duration, string description, DateTime dueDate)
        {
            Id = id;
            Name = name;
            IsComplete = isComplete;
            Duration = duration;
            Description = description;
            DueDate = dueDate;

            Subtasks = new List<TaskModel>();
        }

        public TaskModel()
        {
            Subtasks = new List<TaskModel>();
        }

        public void AddSubtask(TaskModel subtask)
        {
            Subtasks.Add(subtask);
        }
    }
}
