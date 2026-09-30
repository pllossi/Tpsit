namespace ExSemaphore
{
    internal class Program
    {

        static void Main(string[] args)
        { 
            Parallel.Invoke(Es.Metti, Es.Togli); //chiamo le funzioni in parallelo
        }
    }
    static class Es
    {
        static int NumVal = new Random().Next(1, 100); //genero un numero casuale di valori da inserire nel buffer
        static int buffer;
        static SemaphoreSlim bufferVuoto = new SemaphoreSlim(1); //inizializzo il semaforo a 1 per indicare che il buffer è vuoto
        static SemaphoreSlim bufferPieno = new SemaphoreSlim(0); //inizializzo il semaforo a 0 per indicare che il buffer è pieno
        /// <summary>
        /// Metodo per inserire numeri casuali pari nel buffer, utilizzando un semaforo per gestire l'accesso al buffer condiviso.
        /// </summary>
        public static void Metti()
        {
            List<int> list = new List<int>(); //creo una lista per tenere traccia dei numeri inseriti nel buffer
            for (int i = 0; i < NumVal; i++) //ciclo per inserire i numeri nel buffer
            {
                bufferVuoto.Wait(); //aspetto che il buffer sia vuoto
                int numB = 0; //inizializzo la variabile numB a 0
                do //ciclo per generare un numero casuale pari e non presente nella lista
                {
                    numB = new Random().Next(1, 200);
                } while (numB % 2 != 0 && !list.Contains(numB));
                buffer = numB; //inserisco il numero nel buffer
                list.Add(numB); //aggiungo il numero alla lista
                Console.WriteLine($"Ho messo {buffer}"); 
                bufferPieno.Release(); //rilascio il semaforo per indicare che il buffer è pieno
            }
        }
        /// <summary>
        /// Metodo per rimuovere numeri dal buffer, utilizzando un semaforo per gestire l'accesso al buffer condiviso.
        /// </summary>
        public static void Togli()
        {
            for (int i = 0; i < NumVal; i++) //ciclo per togliere i numeri dal buffer
            {
                bufferPieno.Wait(); //aspetto che il buffer sia pieno
                if (buffer != 0) //controllo che il buffer non sia vuoto per sicurezza
                {
                    Console.WriteLine($"Ho tolto {buffer}");
                    buffer = 0; //svuoto il buffer
                }
                bufferVuoto.Release(); //rilascia il semaforo per indicare che il buffer è vuoto
            }
        }
    }
}
