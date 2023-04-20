using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InterfaceLayer;
using DataLayer;
using InterfaceLayer.Interfaces;

namespace ADHDPlanner_Backend.Controllers
{
    [Route("api/Task")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;
        private readonly ITaskCollection _taskDatabase;

        public TasksController(TaskContext context, ITaskCollection taskDatabase)
        {
            _taskDatabase = taskDatabase;
            _context = context;
        }

        // GET: api/TodoItems
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <remarks>
        /// This endpint can be used to get all existing tasks.
        /// </remarks>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskDTO>))]
        public ActionResult GetTasks()
        {
            List<TaskDTO> tasks = _taskDatabase.GetAllTasks();
            return Ok(tasks);
        }

        // GET: api/TodoItems/5
        /// <summary>
        /// Get tasks by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDTO))]
        public ActionResult GetTask(int id)
        {
            TaskDTO task = _taskDatabase.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/TodoItems/5
        /// <summary>
        /// Update an existing task by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PutTask(int id, TaskDTO taskDTO)
        {
            bool todoItem = _taskDatabase.UpdateTask(id, taskDTO);
            if (todoItem == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        /// <summary>
        /// Make a new task
        /// </summary>
        /// <remarks>
        /// Create a new task with the given parameters. Parameters should be specified in the body of the message.
        /// </remarks>
        /// <param name="taskDTO">Specifies the parameters of the to be created task</param>
        /// <response code="200">The task was sucessfully created</response>
        /// <response code="400">An error was made with the given parameters. More context will be given in the body</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaskDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostTask(TaskDTO taskDTO)
        {
            TaskDTO todoItem = _taskDatabase.CreateTask(taskDTO);

            return Ok(todoItem);
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        /// <summary>
        /// Delete a task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            bool state = _taskDatabase.DeleteTask(id);

            if (!state)
            {
                return NotFound();
            }
            return Ok();
        }

        private bool TaskExists(long id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        private static TaskDTO ItemToDTO(TaskModel task) =>
           new TaskDTO
           {
               Id = task.Id,
               Name = task.Name,
               IsComplete = task.IsComplete,
               Duration = task.Duration,
               DueDate = task.DueDate,
               Description = task.Description
           };
    }
}
