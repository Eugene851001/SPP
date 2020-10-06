using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGenerator;
using System.IO;
using System.Threading;

namespace LoadSaveData
{
    class Program
    {
        static int maxFileLoading = 4;
        static int maxFileWriting = 4;
        static int maxTestsGenerating = 4;

        static int fileLoadingCounter = 0;
        static int testsGeneratingCounter = 0;
        static int fileWritingCounter = 0;

        static void Main(string[] args)
        {

            int fileCounter = 0;
            string outputDirectory = ".";
            TestClassGenerator generator = new TestClassGenerator();

            int maxLoading = 0;

            var downloadString = new TransformBlock<string, Task<string>>(path =>
            {
                while (fileLoadingCounter >= maxFileLoading) ;
                StreamReader sr = new StreamReader(path);
                Task<string> result = sr.ReadToEndAsync();
                fileLoadingCounter++;
                if (maxLoading < fileLoadingCounter)
                    maxLoading = fileLoadingCounter;
                result.ContinueWith(str => { fileLoadingCounter--; });
                return result;
            });

            var generateClass = new TransformBlock<Task<string>, Task<string>>(async sourceTask =>
           {
               while (testsGeneratingCounter >= maxTestsGenerating) ;
               testsGeneratingCounter++;
               Task<string> task = generator.GenerateTestAsync(await sourceTask);
               Task temp = task.ContinueWith(str => { testsGeneratingCounter--;});
               return task;
           });

            var outputString = new ActionBlock<Task<string>>(async sourceTask => 
            {
                string source = await sourceTask;
                while (fileWritingCounter >= maxFileWriting) ;
                fileWritingCounter++;
                fileCounter++;
                StreamWriter sw = new StreamWriter($"{outputDirectory}/Test{fileCounter}.cs");
                Task t = sw.WriteAsync(source);
                Task temp = t.ContinueWith((str) => {fileWritingCounter--; });
                t.Wait();
                sw.Close();
            });

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            downloadString.LinkTo(generateClass, linkOptions);
            generateClass.LinkTo(outputString, linkOptions);

            List<string> fileNames = new List<string>() { "Program.cs", "AssemblyGetter.cs",
                "StudentExtension.cs", "Factorizer.cs" };
            foreach (var name in fileNames)
            {
                downloadString.Post(name);
            }
            downloadString.Complete();
            outputString.Completion.Wait();

            Console.WriteLine(maxLoading);
            Console.ReadLine();
        }
    }
}
