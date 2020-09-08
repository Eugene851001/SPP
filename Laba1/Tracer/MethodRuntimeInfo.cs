using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracert
{
 
    public class MethodRuntimeInfo
    {
        public long EllapsedTime { get; set; }
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        [XmlArrayItem("Method")]
        public List<MethodRuntimeInfo> Methods { get; set; }

        public MethodRuntimeInfo()
        {
            EllapsedTime =  0;
            MethodName = "Unknown";
            ClassName = "Unknown";
            Methods = new List<MethodRuntimeInfo>();
        }
    }
}
