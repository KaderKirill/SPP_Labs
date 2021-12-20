using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SPP_Project2
{
    class Program
    {
        static System.Diagnostics.Stopwatch startTime;
        
        static void Main(string[] args)
        {
            string original = "";
            string final = "";
            Console.WriteLine("Write source Directory");
            original = Console.ReadLine();
            Console.WriteLine("Write final Directory");
            final = Console.ReadLine();

            try
            {
                StartTimer();

                CopyFiles copy = new CopyFiles(@original, @final);
                copy.Copy(true);
                Console.Write("Time : "); StopTimer();
                Console.WriteLine("Threre are copied files : " + copy.getCopied());
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }
        private static void StartTimer()
        {
            startTime = System.Diagnostics.Stopwatch.StartNew();
        }
        private static void StopTimer()
        {
            startTime.Stop();
            var resultTime = startTime.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);
            Console.WriteLine("" + elapsedTime);
        }
    }
}
