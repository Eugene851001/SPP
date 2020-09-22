using System;
using System.Collections.Generic;

namespace DTO
{
    public class DataTypeInfo
    {
        public string Name { get; set; }
        public List<FieldInfo> Fields { get; set; } = new List<FieldInfo>();
        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();
        public List<MethodInfo> Methods { get; set; } = new List<MethodInfo>();
    }
}
