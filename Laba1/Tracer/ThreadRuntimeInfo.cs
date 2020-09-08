using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracert
{
    public class ThreadRuntimeInfo
    {
        public int Id { get; set; }
        public long EllapsedTime { get; set; }
        
        [XmlArrayItem("Method")]
        public List<MethodRuntimeInfo> Methods { get; set; }
    }
}
