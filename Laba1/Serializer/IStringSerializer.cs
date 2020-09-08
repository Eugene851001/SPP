using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    public interface IStringSerializer
    {
        string SerializeString(object obj, Type type, Type[] types = null);
        object DeserializeString(string source, Type type, Type[] types = null);
    }
}
