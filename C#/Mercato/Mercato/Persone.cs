using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercato
{
    public class Persone
    {
        private readonly AutoResetEvent _tickEvent = new AutoResetEvent(false);
        public void PrendiOggetti(List<string> bancone)
        {
            _tickEvent.WaitOne();
            var rand = new Random();
            rand.Next(0, bancone.Count);
            for (int i = 0; i < bancone.Count; i++)
            {
                Console.WriteLine("Preso: " + bancone.First());
                bancone.RemoveAt(0);
                Thread.Sleep(100);
            }
            _tickEvent.Set();
        }
    }
}
