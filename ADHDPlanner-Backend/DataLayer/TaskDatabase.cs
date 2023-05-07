using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using InterfaceLayer;
using Microsoft.EntityFrameworkCore;
using InterfaceLayer.Interfaces;

namespace DataLayer
{
    public class TaskDatabase : ITaskCollection
    {
        private readonly TaskContext _context;

        public TaskDatabase(TaskContext context)
        {
            _context = context;
        }

        public List<TaskDTO> GetAllTasks()
        {
            List<TaskDTO> tasks = _context.Tasks
                .Select(x => ItemToDTO(x))
                .ToList();

            return tasks;
        }

        public TaskDTO? GetTask(int id)
        {
            TaskDTO? task = _context.Tasks.Where(x => x.Id == id).Select(x => ItemToDTO(x)).FirstOrDefault();

            return task;
        }

        public TaskDTO? UpdateTask(int id, TaskDTO task)
        {
            TaskModel? todoItem = _context.Tasks.Where(x => x.Id == id).FirstOrDefault();

            if (todoItem == null)
            {
                return null;
            }

            todoItem.Name = task.Name;
            todoItem.IsComplete = task.IsComplete;
            todoItem.Duration = task.Duration;
            todoItem.DueDate = task.DueDate;
            todoItem.Description = task.Description;

            if (_context.SaveChanges() > 0)
            {
                return task;
            }
            return null;
        }

        public TaskDTO CreateTask(TaskDTO taskDTO)
        {
            TaskModel task = new TaskModel();

            task.IsComplete = taskDTO.IsComplete;
            task.Name = taskDTO.Name;
            task.Duration = taskDTO.Duration;
            task.DueDate = taskDTO.DueDate;
            task.Description = taskDTO.Description;


            _context.Tasks.Add(task);
            _context.SaveChanges();

            return taskDTO;
        }

        public bool DeleteTask(int id)
        {
            TaskModel? todoItem = _context.Tasks.Where(x => x.Id == id).FirstOrDefault();

            if (todoItem == null)
            {
                return false;
            }

            _context.Tasks.Remove(todoItem);

            return _context.SaveChanges() > 0;
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
