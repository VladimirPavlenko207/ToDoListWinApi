using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Models.Entities;
using ToDoListApi.Models.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly BaseContext _context;
        public TaskController(BaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.Include(t => t.Category).
                Include(t => t.Tags).ThenInclude(tag => tag.Tasks).ToList();
            return new JsonResult(tasks.OrderBy(t => t.IsComplete).ThenByDescending(t => t.Priority),
                new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });
        }

        [HttpGet]
        [Route("getById")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Include(t => t.Category).FirstOrDefault(t => t.Id == id);
            if(task is null) return BadRequest(new { errorTexr = "Invalid task id" });
            return new JsonResult(task);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult NewCategory([FromBody] TaskModel model)
        {
            var task = new MyTask()
            {
                Category = _context.Categories.FirstOrDefault(c => c.Name == model.CategoryName),
                Description = model.Description,
                Deadline = Convert.ToDateTime(model.Deadline),
                Priority = (ThreadPriority)model.Priority,
                IsComplete = false,
                Tags = _context.Tags.Where(t => model.TagsNames.Any(n => n == t.Name)).ToList()
            };
            _context.Tasks.Add(task);
            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditTask([FromBody] TaskModel model) 
        {
            var task = _context.Tasks.Include(t => t.Tags).ThenInclude(t => t.Tasks).FirstOrDefault(t => t.Id == model.Id);
            task.Description = model.Description;
            task.Deadline = Convert.ToDateTime(model.Deadline);
            task.Category = _context.Categories.FirstOrDefault(c => c.Name == model.CategoryName);
            task.Priority = model.Priority is not null ? (ThreadPriority)model.Priority : null;
            task.IsComplete = model.IsComplete;
            task.Tags = _context.Tags.Where(t => model.TagsNames.Any(n => n == t.Name)).ToList();

            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult RemoveTask(int id)
        {
            _context.Tasks.Remove(_context.Tasks.FirstOrDefault(t => t.Id == id));
            _context.SaveChanges();
            return _context.SafePreservation();
        }

    }
}
