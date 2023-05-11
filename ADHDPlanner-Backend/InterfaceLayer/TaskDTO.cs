namespace InterfaceLayer
{
    public class TaskDTO
    {
        /// <summary>
        /// Task parameters
        /// </summary>
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int? Duration { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public List<TaskDTO>? Subtasks { get; set; }
    }
}
