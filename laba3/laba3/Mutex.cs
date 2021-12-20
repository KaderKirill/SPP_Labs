using System;
using System.Threading;

namespace laba3
{
	public class Mutex
    {
		private int curID = 0;
        
		public Mutex()
		{
        }
        
		public void Lock(int id)
		{
			do
			{
			} while (Interlocked.CompareExchange(ref curID, id, 0) != 0);      
		}

		public void Unlock(int id)
		{
			Interlocked.CompareExchange(ref curID, 0, id);
		}
    }
}
