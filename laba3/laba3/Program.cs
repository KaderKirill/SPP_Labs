using System.Threading;
using System;

namespace laba3
{
	class MainClass 
    {
		static Mutex mutex = new Mutex();
		static int x = 0;
        
		public static void Main()
        {
			for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }

			Thread.Sleep(1000);           
			Console.WriteLine("THE END");
        }

		public static void Count()
        {
			
			mutex.Lock(Thread.CurrentThread.ManagedThreadId);
            
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }

			mutex.Unlock(Thread.CurrentThread.ManagedThreadId);
        }
    }
}