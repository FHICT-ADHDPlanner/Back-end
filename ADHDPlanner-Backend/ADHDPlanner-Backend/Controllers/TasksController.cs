using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ADHDPlanner_Backend.Models;

namespace ADHDPlanner_Backend.Controllers
{
    [Route("api/Tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _context;

        public TasksController(TaskContext context)
        {
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskModel>))]
        public ActionResult GetTasks()
        {
           List<TaskDTO> tasks = _context.Tasks
                .Select(x => ItemToDTO(x))
                .ToList();
            return Ok(tasks);
        }

        // GET: api/TodoItems/5
        /// <summary>
        /// Get tasks by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TaskModel>))]
        public ActionResult GetTask(long id)
        {
            var task = _context.Tasks.FindAsync(id);

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
        public IActionResult PutTask(long id, TaskDTO taskDTO)
        {
            if (id != taskDTO.Id)
            {
                return BadRequest();
            }

            TaskModel todoItem = _context.Tasks.Where(x => x.Id == id).First();
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = taskDTO.Name;
            todoItem.IsComplete = taskDTO.IsComplete;
            todoItem.Duration = taskDTO.Duration;
            todoItem.DueDate = taskDTO.DueDate;
            todoItem.Description = taskDTO.Description;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) when (!TaskExists(id))
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
            var task = new TaskModel
            {
                IsComplete = taskDTO.IsComplete,
                Name = taskDTO.Name,
                Duration = taskDTO.Duration,
                DueDate = taskDTO.DueDate,
                Description = taskDTO.Description,
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                ItemToDTO(task));
        }
        // </snippet_Create>

        // DELETE: api/TodoItems/5
        /// <summary>
        /// Delete a task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(long id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
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
