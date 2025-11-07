using System;
using System.Threading;

namespace EsThread
{
    class Program
    {
        static void Main(string[] args)
        {
            object res1 = new object();
            object res2 = new object();

            var t1 = new Thread(() =>
            {
                Console.WriteLine("Thread 1: locking res1");
                lock (res1)
                {
                    Console.WriteLine("Thread 1: locked res1");
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("Thread 1: waiting for res2");
                    lock (res2)
                    {
                        Console.WriteLine("Thread 1: locked res2");
                    }
                }
            });

            var t2 = new Thread(() =>
            {
                Console.WriteLine("Thread 2: locking res2");
                lock (res2)
                {
                    Console.WriteLine("Thread 2: locked res2");
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("Thread 2: waiting for res1");
                    lock (res1)
                    {
                        Console.WriteLine("Thread 2: locked res1");
                    }
                }
            });

            t1.Start();
            t2.Start();

            // i Join restano bloccati se si verifica il deadlock
            t1.Join();
            t2.Join();
            //devo fare 2 thread che finiscono in deadlock per richiesta della stessa risorsa

        }
    }
}