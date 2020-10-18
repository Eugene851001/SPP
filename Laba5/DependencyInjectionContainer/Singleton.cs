using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{

    class Singleton
    {

        /// <summary>
        /// Describes factory method
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public delegate object CreateInstance(Type type);

        static object instance = null;
        static object syncRoot = new object();

        public static object GetInstance(Type type, CreateInstance createInstance)
        {
            if(instance == null)
            {
                lock(syncRoot)
                {
                    if(instance == null)
                    {
                        instance = createInstance(type);
                    }
                }
            }
            return instance;
        }
    }
}
