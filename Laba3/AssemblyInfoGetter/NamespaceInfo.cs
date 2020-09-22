using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NamespaceInfo
    {
        public string Name { get; set; }
        public List<DataTypeInfo> DataTypes { get; set; } = new List<DataTypeInfo>();
    }
}
