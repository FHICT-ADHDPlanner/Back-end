using InterfaceLayer;
using InterfaceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDPlanner_BackendTests.Mock
{
    public class MockTaskDatabase : ITaskCollection
    {
        public List<TaskDTO> Tasks { get; set; }


        public MockTaskDatabase()
        {
            Tasks = new List<TaskDTO>();
        }


        public List<TaskDTO> GetAllTasks()
        {
            return Tasks;
        }

        public TaskDTO? GetTask(int id)
        {
            return Tasks.Find(x => x.Id == id);
        }

        public bool UpdateTask(int id, TaskDTO task)
        {
            TaskDTO? dto = Tasks.Find(x => x.Id == id);

            if (dto == null)
                return false;


            dto.Name = task.Name;

            return true;
        }

        public TaskDTO CreateTask(TaskDTO task)
        {
            Tasks.Add(task);
            return task;
        }

        public bool DeleteTask(int id)
        {
            TaskDTO? dto = Tasks.Find(x => x.Id == id);

            if (dto == null)
                return false;


            Tasks.Remove(dto);
            return true;
        }
    }
}

