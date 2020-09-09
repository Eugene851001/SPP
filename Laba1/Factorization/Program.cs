using System;
using Tracert;
using Serializer;
using System.Threading;
using System.IO;
using System.Text;

namespace Factorization
{
    class Program
    {
        static string outputFileNameJson = "outputJson.txt";
        static string outputFileNameXml = "outputXml.txt";
        static bool isResultReady = false;
        static ITracer tracer;
        static void BusyMethod()
        {

            tracer.StartTrace();
            Pow(2, 20);
            sleep200();
            tracer.StopTrace();
            isResultReady = true;
        }

        static void sleep200()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            sleep300();
            tracer.StopTrace();
        }

        static void sleep300()
        {
            tracer.StartTrace();
            Thread.Sleep(300);
            tracer.StopTrace();
        }

        static int Pow(int a, int n)
        {
            tracer.StartTrace();
            int x = a;
            for (int i = 0; i < n; i++)
            {
                x *= a;
            }
            tracer.StopTrace();
            return x;
        }

        static void writeFile(string fileName, string info)
        {
            FileStream f = new FileStream(fileName, FileMode.Create);
            try
            {
                f.Write(Encoding.UTF8.GetBytes(info), 0, info.Length);
            }
            finally
            {
                f.Close();
            }
        }

        static void Main(string[] args)
        {
            Divider[] dividers = new Divider[100];
            tracer = new Tracer();
            Factorizer factorizer = new Factorizer(tracer);
            Console.WriteLine("Enter integer: ");
            int num = int.Parse(Console.ReadLine());
            int divAmount = factorizer.factorize(num, dividers);
            for (int i = 0; i < divAmount - 1; i++)
            {
                Console.Write(dividers[i].Base + "^" + dividers[i].Degree + "*");
            }
            isResultReady = false;
            Thread thread = new Thread(BusyMethod);
            thread.Start();
            Thread.Sleep(1000);
            BusyMethod();
            Console.WriteLine(dividers[divAmount - 1].Base + "^" + dividers[divAmount - 1].Degree);

            while (!isResultReady) ;
            IStringSerializer serializer = new SerializerJson();
            string runtimeInfo = serializer.SerializeString(tracer.GetTraceResult(), typeof(TraceResult));
            Console.WriteLine(runtimeInfo);
            writeFile(outputFileNameJson, runtimeInfo);
            serializer = new SerializerXml();
            runtimeInfo = serializer.SerializeString(tracer.GetTraceResult(), typeof(TraceResult));
            Console.WriteLine(runtimeInfo);
            writeFile(outputFileNameXml, runtimeInfo);
            Console.ReadLine();
        }

    }
}
