using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApi.Helpers
{
    public static class ObjectPropOperations
    {
        public static void SetPropertyValue<T>(T value, ref T prop)
        {
            if (value is not null) 
                prop = value; 
        }
    }
}
