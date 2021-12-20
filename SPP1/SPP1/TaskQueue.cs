using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SPP_Project1
{

    class TaskQueue
    {
        private int count;
        public delegate void TaskDelegate();
        static object locker = new object();
        Queue<Thread> pull;
        Queue<TaskDelegate> tasks;
        private static int inprogress;

        public TaskQueue(int count)
        {
            inprogress = 0;
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
            TaskDelegate curtask;
            while (true)
            {
                bool acquiredLock = false;
                try
                {
                    Monitor.Enter(locker, ref acquiredLock);
                    while (tasks.Count == 0)
                    {
                    }
                    curtask = tasks.Dequeue();
                }
                finally
                {
                    if (acquiredLock) Monitor.Exit(locker);
                }

                if (curtask != null)
                {
                    curtask();
                    inprogress--;
                    curtask = null;

                }
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            tasks.Enqueue(task);
            inprogress++;
        }

        public void Finalize()
        {
            int size = pull.Count();
            for (int i = 0; i < size; i++)
            {
                while (inprogress > 0)
                {
                }
                pull.Dequeue();
            }
        }

    }
}