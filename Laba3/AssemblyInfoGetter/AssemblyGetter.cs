using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoGetter
{
    public class AssemblyGetter
    {
        Assembly assembly;
        
        public void LoadAssembly(string fileName)
        {
            assembly = Assembly.LoadFrom(fileName);
        }

        public DTO.NamespaceInfo GetAssemblyInfo()
        {
            DTO.NamespaceInfo namespaceInfo = new DTO.NamespaceInfo();
            namespaceInfo.Name = assembly.FullName;
            foreach(Type type in assembly.GetTypes())
            {
                var dataTypeInfo = new DTO.DataTypeInfo();
                dataTypeInfo.Name = type.Name;
                foreach(var memberInfo in type.GetMembers())
                {
                    if (memberInfo is FieldInfo)
                    {
                        var fieldInfo = new DTO.FieldInfo();
                        fieldInfo.Name = memberInfo.Name;
                        fieldInfo.Type = ((FieldInfo)memberInfo).FieldType;
                        dataTypeInfo.Fields.Add(fieldInfo);
                    }
                    else if(memberInfo is PropertyInfo)
                    {
                        var propertyInfo = new DTO.PropertyInfo();
                        propertyInfo.Name = memberInfo.Name;
                        propertyInfo.Type = ((PropertyInfo)memberInfo).PropertyType;
                        dataTypeInfo.Properties.Add(propertyInfo);
                    }
                }
                foreach(var method in type.GetMethods())
                {
                    var methodInfo = new DTO.MethodInfo();
                    methodInfo.Name = method.Name;
                    methodInfo.ReturnType = method.ReturnType;
                    foreach(var parameter in method.GetParameters())
                    {
                        var parameterInfo = new DTO.ParameterInfo() 
                        { Name = parameter.Name, Type = parameter.ParameterType };
                        methodInfo.Parameters.Add(parameterInfo);
                    }
                    dataTypeInfo.Methods.Add(methodInfo);
                    if (method.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        methodInfo.Name += " Extends ";
                        methodInfo.Name += method.GetParameters()[0].ParameterType.Name;
                    }
                }
                namespaceInfo.DataTypes.Add(dataTypeInfo);
            }
            return namespaceInfo;
        }

        public List<System.Reflection.MethodInfo> GetExtensionMethods()
        {
            
            var extensionMethods = new List<System.Reflection.MethodInfo>();
            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    if (method.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        extensionMethods.Add(method);
                    }
                }
            }
            return extensionMethods;
        }

        public void AddExtensionMethod(DTO.NamespaceInfo namespaceInfo, MethodInfo extensionMethod)
        {
            foreach(var dataType in namespaceInfo.DataTypes)
            {
                if(dataType.Name == extensionMethod.GetParameters()[0].ParameterType.Name)
                {
                    dataType.Methods.Add(new DTO.MethodInfo() { Name = extensionMethod.Name + "- Extesnion" });
                }
            }
        }

        public AssemblyGetter()
        {
            
        }
    }
}
