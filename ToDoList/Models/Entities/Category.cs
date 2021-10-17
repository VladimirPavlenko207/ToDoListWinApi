using Microsoft.EntityFrameworkCore;
using System;
using ToDoListApi.Helpers;

namespace ToDoList.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        private string name;
        public int Id { get; set; }
        public string Name { get => name; set => ObjectPropOperations.SetPropertyValue(value, ref name); }
    }
}
