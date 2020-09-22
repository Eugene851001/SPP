using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace DTO
{
    public class DataTypeInfo
    {
        public string Name { get; set; }
        public List<FieldInfo> Fields { get; set; } = new List<FieldInfo>();
        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();
        public List<MethodInfo> Methods { get; set; } = new List<MethodInfo>();

        public IList MembersInfo
        {
            get
            {
                return new CompositeCollection()
                {
                    new CollectionContainer(){Collection = Fields},
                    new CollectionContainer(){Collection = Properties},
                    new CollectionContainer(){Collection = Methods}
                };
            }
        }
    }
}
