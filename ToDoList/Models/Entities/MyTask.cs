using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using ToDoListApi.Helpers;
using ToDoListApi.Models.Entities;

namespace ToDoList.Models.Entities
{
    [Index(nameof(Description), IsUnique = true)]
    public class MyTask
    {
        private string description;
        private ThreadPriority? priority;
        private DateTime? deadline;
        private Category category;

        public int Id { get; set; }
        public string Description { get => description; set => ObjectPropOperations.SetPropertyValue(value, ref description); }
        public ThreadPriority? Priority { get => priority; set => ObjectPropOperations.SetPropertyValue(value, ref priority); }
        public DateTime? Deadline { get => deadline; set => ObjectPropOperations.SetPropertyValue(value, ref deadline); }
        public Category Category { get => category; set => ObjectPropOperations.SetPropertyValue(value, ref category); }
        public bool IsComplete { get; set; }
        public List<Tag> Tags { get; set; }

    }
}
