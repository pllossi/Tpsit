using System;
using System.Collections.Generic;
using System.Threading;

namespace Mercato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent _tickEvent = new AutoResetEvent(true);
            List<string> bancone = new List<string>();
            Rifornitore rifornitore = new Rifornitore();
            Persone persone = new Persone();
            MercatoStato stato = new MercatoStato();

            Thread t1 = new Thread(c => rifornitore.MettiOggetti(bancone, _tickEvent));
            Thread t2 = new Thread(c => persone.PrendiOggetti(bancone, _tickEvent, stato));
            t1.Start();
            t2.Start();

            while (true)
            {
                Thread.Sleep(10000);
                stato.Close();
                Console.WriteLine("Mercato chiuso — nessun altro acquisto possibile.");
                Thread.Sleep(5000);
                stato.Open();
                Console.WriteLine("Mercato riaperto — gli acquisti possono riprendere.");
            }

        }
    }
}