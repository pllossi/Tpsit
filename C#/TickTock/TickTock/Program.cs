using System;
using System.Threading;

namespace TickTock
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock c = new Clock();
            Thread t1 = new Thread(c.Tick);
            Thread t2 = new Thread(c.Tock);
            t1.Start();
            t2.Start();
        }

    }
}