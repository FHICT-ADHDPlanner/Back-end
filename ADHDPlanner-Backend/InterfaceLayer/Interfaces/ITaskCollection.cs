using System.Transactions;

namespace InterfaceLayer.Interfaces
{
    public interface ITaskCollection
    {
        List<TaskDTO> GetAllTasks();
        TaskDTO? GetTask(int id);
        TaskDTO? UpdateTask(int id, TaskDTO task);
        TaskDTO CreateTask(TaskDTO task);
        bool DeleteTask(int id);
    }
}