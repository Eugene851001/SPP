using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DependencyInjectionContainer
{
    public enum ImplementationName
    {
        None,
        First,
        Second
    }

    public class DependenciesConfiguration
    {
        public Dictionary<Type, ImplementationInfo> dependencies { get; } 
            = new Dictionary<Type, ImplementationInfo>();
        public Dictionary<Type, ImplementationInfo> openGenericDependencies { get; }
            = new Dictionary<Type, ImplementationInfo>();

        void Register<TDependency>(ImplementationInfo implementationInfo)
        {
            if (dependencies.ContainsKey(typeof(TDependency)))
                dependencies[typeof(TDependency)].Add(implementationInfo);
            else
                dependencies.Add(typeof(TDependency), implementationInfo);
        }

        public void RegisterSingleton<TDependency, TImplementation>(
            ImplementationName implementationName = ImplementationName.None) where TImplementation: TDependency
        {
            var implementationInfo = new ImplementationInfo()
                { Lifetime = Lifetime.Singleton, TImplementation = typeof(TImplementation), ImplementationName = implementationName};
            Register<TDependency>(implementationInfo);

        }

        public void RegisterTransient<TDependency, TImplementation>(
            ImplementationName implementationName = ImplementationName.None) where TImplementation: TDependency
        {
            var implementationInfo = new ImplementationInfo()
                { Lifetime = Lifetime.Transient, TImplementation = typeof(TImplementation), ImplementationName = implementationName };
            Register<TDependency>(implementationInfo);

        }

        public void Register(Type typeDependency, Type typeImplementation)
        {
            var implemetationInfo = new ImplementationInfo()
                {Lifetime = Lifetime.Transient, TImplementation = typeImplementation};
            if (openGenericDependencies.ContainsKey(typeDependency))
                openGenericDependencies[typeDependency].Add(implemetationInfo);
            else
                openGenericDependencies.Add(typeDependency, implemetationInfo);

        }

    }
}
