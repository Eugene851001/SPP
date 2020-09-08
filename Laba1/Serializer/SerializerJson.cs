using System;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Serializer
{
    public class SerializerJson: IStringSerializer
    {
        public string SerializeString(object obj, Type type, Type[] types = null)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public object DeserializeString(string source, Type type, Type[] types = null)
        {
            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(source));
            var jsonSerializer = new DataContractJsonSerializer(type, types);
            return jsonSerializer.ReadObject(stream);
        }

    }
}