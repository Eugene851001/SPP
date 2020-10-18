using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class GenericSingleton<T> where T: class, new()
    {
        static T instance = null;
        static object syncRoot = new object();

        static public T GetInstance()
        {
            if(instance != null)
            {
                lock(syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }

    }
}
