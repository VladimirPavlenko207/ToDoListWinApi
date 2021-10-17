using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ToDoList.Models.Entities;
using ToDoListApi.Models;
using ToDoListApi.Models.Entities;

namespace ToDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly BaseContext _context;
        public TagController(BaseContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            var tags = _context.Tags.Include(t => t.Tasks).ToList();
            return new JsonResult(tags,
                new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve });
        }

        [HttpPost]
        [Route("create")]
        public IActionResult NewTag([FromBody] TagModel model)
        {
            var tag = new Tag() { Name = model.Name };
            _context.Tags.Add(tag);
            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditTag([FromBody] TagModel model)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Name == model.Name);
            tag.Name = model.NewName;
            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult RemoveTag(string name)
        {
            _context.Tags.Remove(_context.Tags.FirstOrDefault(c => c.Name == name));
            _context.SaveChanges();
            return _context.SafePreservation();
        }
    }
}
