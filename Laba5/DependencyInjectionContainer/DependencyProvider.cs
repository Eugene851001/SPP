using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DependencyInjectionContainer
{

    public class DependencyProvider
    {

        Dictionary<Type, ImplementationInfo> dependencies;
        Dictionary<Type, ImplementationInfo> openGenericDependecies;

        public DependencyProvider(DependenciesConfiguration configuration)
        {
            dependencies = configuration.dependencies;
            openGenericDependecies = configuration.openGenericDependencies;
        }

        object Resolve(Type serviceType)
        {
            if(serviceType.IsGenericType &&
                ( serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return GetEnumerable(serviceType.GetGenericArguments()[0]);
            }
            if(serviceType.IsGenericType && 
                openGenericDependecies.ContainsKey(serviceType.GetGenericTypeDefinition()))
            {
                Type implemetationType = openGenericDependecies[serviceType.GetGenericTypeDefinition()].TImplementation;
                implemetationType = implemetationType.MakeGenericType(serviceType.GetGenericArguments());
                var implemetation = new ImplementationInfo()
                    { Lifetime = Lifetime.Transient, TImplementation = implemetationType };
                dependencies.Add(serviceType, implemetation);
            }
            if (!dependencies.ContainsKey(serviceType))
                return null;
            var implemtationInfo = dependencies[serviceType];
            if (!serviceType.IsAssignableFrom(implemtationInfo.TImplementation))
                return null;
            if (implemtationInfo.Lifetime == Lifetime.Singleton)
                return Singleton.GetInstance(implemtationInfo.TImplementation, CreateImplemetationInstance);
            if (implemtationInfo.Lifetime == Lifetime.Transient)
                return CreateImplemetationInstance(implemtationInfo.TImplementation);
            return null;
        }

        public IEnumerable GetEnumerable(Type type)
        {
            IEnumerable<ImplementationInfo> implemetations = dependencies[type].ImplementationList();
            Type returnType = typeof(List<>).MakeGenericType(type);
            IList list = (IList)Activator.CreateInstance(returnType);
            foreach(var implementation in implemetations)
            {
                list.Add(CreateImplemetationInstance(implementation.TImplementation));

            }
            return list;
        }

        public T Resolver<T>()
        {
            /*     if(typeof(T) is IEnumerable)
                 {

                     return GetEnumerable<>();
                 }*/ 
            Type serviceType = typeof(T);
            return (T)Resolve(serviceType);
        }

        object Resolver(Type serviceType)
        {
            return Resolve(serviceType);
        }



        object CreateImplemetationInstance(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            var bestConstructor = constructors[0];   
            foreach(var constructor in constructors)
            {
                if(constructor.GetParameters().Length > bestConstructor.GetParameters().Length)
                {
                    bestConstructor = constructor;
                }
            }
           

            List<object> parameters = new List<object>();
            foreach(ParameterInfo parameter in bestConstructor.GetParameters())
            {
                if(dependencies.ContainsKey(parameter.ParameterType))
                {
                    parameters.Add(Resolver(parameter.ParameterType));
                }
                else
                {
                    parameters.Add(null);
                }
            }
            return Activator.CreateInstance(type, parameters.ToArray());
        }

    }
}
