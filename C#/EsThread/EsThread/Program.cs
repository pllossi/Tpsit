using System;

namespace EsThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(Test.Method);
            Console.WriteLine("Starting thread...");
            t.Start();
            Console.WriteLine(t.ThreadState);
            Thread.Sleep(30000); 
            Console.WriteLine("Main thread ending.");
            t.Join();
        }
    }

    static class Test
    {
        public static void Method()
        {
            Console.WriteLine("Test Method");
        }
    }
}