using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mercato
{
    public class Rifornitore
    {
        private readonly AutoResetEvent _tickEvent = new AutoResetEvent(true);
        public void MettiOggetti(List<string> bancone)
        {
            _tickEvent.WaitOne();
            var rand = new Random();
            while (bancone.Count != 20)
            {
                rand.Next(97, 123);
                bancone.Add(Convert.ToString((char)rand.Next(97, 123)));
                Console.WriteLine("Aggiunto: " + bancone.Last());
                Thread.Sleep(50);
            }
            _tickEvent.Set();
        }
    }
}
