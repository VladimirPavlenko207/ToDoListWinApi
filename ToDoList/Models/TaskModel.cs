using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Models.Entities;
using ToDoListApi.Models.Entities;

namespace ToDoList.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Priority { get; set; }
        public string Deadline { get; set; }
        public string CategoryName { get; set; }
        public bool IsComplete { get; set; }
        public List<string> TagsNames { get; set; }
    }
}
