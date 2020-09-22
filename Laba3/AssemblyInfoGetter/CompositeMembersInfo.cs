using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CompositeMembersInfo
    {
        public FieldInfo Field { get; set; }
        public MethodInfo Method { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
