using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DependencyKeyAttribute: Attribute
    {
        public ImplementationName ImplementationName { get; }

        public DependencyKeyAttribute(ImplementationName implementationName)
        {
            ImplementationName = implementationName;
        }
    }
}
