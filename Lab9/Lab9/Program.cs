using System;

namespace NinthTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var dynamicList = new DynamicList<int>();
            Console.WriteLine(dynamicList.Count);

            for (int index = 1; index <= 10; index++)
            {
                dynamicList.Add(index);
            }
            foreach (var element in dynamicList)
            {
                Console.Write(element + " ");
            }

            Console.WriteLine("\n");
            Console.WriteLine(dynamicList.Count);
            dynamicList.Remove(4);
            Console.WriteLine(dynamicList.Count);

            foreach (var element in dynamicList)
            {
                Console.Write(element + " ");
            }

            Console.WriteLine(dynamicList[1]);

            dynamicList.Clear();

            Console.WriteLine(dynamicList.Count);
        }
    }
}