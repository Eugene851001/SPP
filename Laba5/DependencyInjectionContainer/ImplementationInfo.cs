using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public enum Lifetime
    {
        Transient,
        Singleton
    }

    public class ImplementationInfo
    {
        public Lifetime Lifetime { get; set; }
        public Type TImplementation;
        public ImplementationInfo NextImplementation = null;
        public ImplementationName ImplementationName = ImplementationName.None;

        public void Add(ImplementationInfo implementation)
        {
            ImplementationInfo listRunner = this;
            while(listRunner.NextImplementation != null)
            {
                listRunner = listRunner.NextImplementation;
            }
            listRunner.NextImplementation = implementation;
        }

        public IEnumerable<ImplementationInfo> ImplementationList()
        {
            ImplementationInfo runner = this;
            while(runner != null)
            {
                yield return runner;
                runner = runner.NextImplementation;
            }
        }
    }
}
