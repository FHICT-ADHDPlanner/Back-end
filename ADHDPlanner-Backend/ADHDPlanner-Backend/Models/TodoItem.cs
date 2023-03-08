namespace ADHDPlanner_Backend.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Secret { get; set; }
    }
}
