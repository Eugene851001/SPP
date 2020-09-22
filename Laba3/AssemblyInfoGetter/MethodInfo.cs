using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MethodInfo
    {
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public List<ParameterInfo> Parameters { get; set; } = new List<ParameterInfo>();
        public bool IsExtension { get; set; } = false;
    }
}
