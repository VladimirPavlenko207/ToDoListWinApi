using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BaseContext _context;
        public CategoryController( BaseContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return new JsonResult(categories);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult NewCategory([FromBody] CategoryModel model)
        {
            var category = new Category() { Name = model.Name };
            _context.Categories.Add(category);
            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult EditCategory([FromBody] CategoryModel model)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == model.Name);
            if (category is null) return BadRequest(new { errorTexr = $"Invalid category {model.Name}" });
            category.Name = model.NewName;
            return _context.SafePreservation();
        }

        [HttpPost]
        [Route("remove")]
        public IActionResult RemoveCategory(string name)
        {
            _context.Categories.Remove(_context.Categories.FirstOrDefault(c => c.Name == name));
            return _context.SafePreservation();
        }
    }
}
