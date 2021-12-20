using System;
using System.Threading;

namespace SeventhTask
{
    class Program
    {

        static void Main(string[] args)
        {
            Parallel.WaitAll(new ParallelDelegate[] { Sleep1, Sleep2, Sleep3 });
            Console.WriteLine("Program: finished.");
        }

        private static void Sleep1()
        {
            Console.WriteLine("Sleep1: started.");
            Thread.Sleep(1000);
            Console.WriteLine("Sleep1: finished.");
        }

        private static void Sleep2()
        {
            Console.WriteLine("Sleep2: started.");
            Thread.Sleep(2000);
            Console.WriteLine("Sleep2: finished.");
        }
        private static void Sleep3()
        {
            Console.WriteLine("Sleep3: started.");
            Thread.Sleep(3000);
            Console.WriteLine("Sleep3: finished.");
        }
    }
}