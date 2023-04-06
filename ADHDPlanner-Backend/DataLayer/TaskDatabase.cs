﻿using System;
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
