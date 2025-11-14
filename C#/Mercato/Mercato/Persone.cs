using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mercato
{
    public class Persone
    {
        public void PrendiOggetti(List<string> bancone, AutoResetEvent _tickEvent, MercatoStato stato)
        {
            while (true)
            {
                _tickEvent.WaitOne();

                while (bancone.Count > 0 && stato.IsOpen)
                {
                    Console.WriteLine("Rimosso: " + bancone.First());
                    bancone.RemoveAt(0);
                    Thread.Sleep(1000);
                }

                _tickEvent.Set();
            }
        }
    }
}