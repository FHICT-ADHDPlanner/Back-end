using Microsoft.EntityFrameworkCore;

namespace ADHDPlanner_Backend.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {

        }

        public DbSet<TaskModel> Tasks { get; set; } = null!;
    }
}
