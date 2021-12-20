using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPP_Project2
{

    class TaskQueue
    {
        private int count;
        private int copied;
        public delegate int TaskDelegate(Queue<string> s);
        static object locker = new object();
        static object incr = new object();
        Queue<Thread> pull;
        Queue<TaskDelegate> tasks;
        Queue<Queue<string>> paths;
        private static int inprogress;

        public TaskQueue(int count)
        {
            inprogress = 0;
            copied = 0;
            paths = new Queue<Queue<string>>();
            tasks = new Queue<TaskDelegate>();
            pull = new Queue<Thread>();
            this.count = count;
            Thread init = new Thread(Init);
            init.IsBackground = true;
            init.Start();
        }
        public void Init()
        {
            for (int i = 0; i < count; i++)
            {
                Thread th = new Thread(WaitTask);
                th.IsBackground = true;
                th.Name = i + "";
                th.Start();
                pull.Enqueue(th);
            }
        }
       
        public void WaitTask()
        {
            TaskDelegate curtask=null;
            Queue<string> tmp;
            while (true)
            {
                bool acquiredLock = false;
                try
                {
                    while (tasks.Count == 0)
                    {
                    }
                    Monitor.Enter(locker, ref acquiredLock);
                    tmp = paths.Dequeue();
                    curtask = tasks.Dequeue();
                        
                }
                finally
                {
                    if (acquiredLock) Monitor.Exit(locker);
                }

                if (curtask != null)
                {
                    int a =curtask(tmp);
                    Monitor.Enter(incr);
                    copied += a;
                    inprogress--;
                    Monitor.Exit(incr);
                    curtask = null;
                }
            }
        }

        public int getCopied() {
            return copied;
        }

        public void Finalize()
        {
            int size = pull.Count();
            while (inprogress > 0 || tasks.Count > 0) {
                
            }
            for (int i = 0; i < size; i++) {
                pull.Dequeue();
            }
        }

        public void EnqueueTask(TaskDelegate task,Queue<string> s)
        {
            paths.Enqueue(s);
            tasks.Enqueue(task);
            inprogress++;
        }
    }
}
