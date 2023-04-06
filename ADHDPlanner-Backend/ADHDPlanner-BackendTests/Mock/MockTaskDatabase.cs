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
    }
}
