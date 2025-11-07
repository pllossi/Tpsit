using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TickTock
{
    public class Clock
    {
        private readonly AutoResetEvent _tickEvent = new AutoResetEvent(true);
        public void Tick()
        {
            while (true)
            {
                _tickEvent.WaitOne();
                Console.WriteLine("Tick");
                Thread.Sleep(1000);
                _tickEvent.Set();
            }
        }
        public void Tock()
        {
            while (true)
            {
                _tickEvent.WaitOne();
                Console.WriteLine("Tock");
                Thread.Sleep(1000);
                _tickEvent.Set();
            }
        }
    }
}
