using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AssemblyInfo
    {
        public string Name { get; set; }
        public List<NamespaceInfo> Namespaces { get; set; } = new List<NamespaceInfo>();
    }
}
