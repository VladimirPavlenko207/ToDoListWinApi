using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ToDoList.Models.Entities;
using ToDoListApi.Helpers;

namespace ToDoListApi.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Tag
    {
        private string name;

        public int Id { get; set; }
        public string Name { get => name; set => ObjectPropOperations.SetPropertyValue(value, ref name); }
        public List<MyTask> Tasks { get; set; } = new();
    }
}
