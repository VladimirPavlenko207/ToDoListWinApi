using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApi.Models.Entities;

namespace ToDoList.Models.Entities
{
    public class BaseContext : DbContext
    {
        public DbSet<MyTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public BaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=127.0.0.1;Database=ToDoListDb;User ID=user1;Password=qwerty;");
        }

        public IActionResult SafePreservation()
        {
            try
            {
                SaveChanges();
                return new OkObjectResult("Changes saved.");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException.Message);
            }
        }
    }
}
