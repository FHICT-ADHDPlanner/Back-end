namespace ADHDPlanner_Backend.Models
{
    public class TodoItemDTO
    {
        /// <summary>
        /// This does something
        /// </summary>
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
