namespace Mercato
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> bancone= new List<string>();
            Rifornitore rifornitore = new Rifornitore();
            Persone persone = new Persone();
            Thread t1 = new Thread(c => rifornitore.MettiOggetti(bancone));
            Thread t2 = new Thread(c => persone.PrendiOggetti(bancone));
            t1.Start();
            t2.Start();
        }
    }
}
