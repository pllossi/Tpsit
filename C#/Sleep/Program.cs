using System;

namespace Sleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sleeping for 5 seconds...");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Awake now!");
        }
    }
}