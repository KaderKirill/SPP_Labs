using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SeventhTask
{
    public delegate void ParallelDelegate();

    public class Parallel
    {
        private class Wrapper
        {
            public Wrapper(int value)
            {
                Value = value;
            }
            public int Value;
        }

        public static void WaitAll(ParallelDelegate[] delegates)
        {
            var resetEvent = new ManualResetEvent(false);
            var threadsCountWrapper = new Wrapper(delegates.Length);
            foreach (var parallelDelegate in delegates)
            {
                var info = new Tuple<ParallelDelegate, Wrapper, ManualResetEvent>(parallelDelegate, threadsCountWrapper, resetEvent);
                ThreadPool.QueueUserWorkItem(ParallelDelegateWrapper, info);
            }
            resetEvent.WaitOne();
        }

        private static void ParallelDelegateWrapper(object info)
        {
            var tupleInfo = info as Tuple<ParallelDelegate, Wrapper, ManualResetEvent>;
            if (tupleInfo == null)
            {
               // throw new Exception();
            }
            tupleInfo.Item1();
            if (Interlocked.Decrement(ref tupleInfo.Item2.Value) == 0)
            {
                tupleInfo.Item3.Set();
            }
        }
    }
}