namespace ExSemaphore
{
    internal class Program
    {

        static void Main(string[] args)
        { 
            Parallel.Invoke(Es.Metti, Es.Togli);
        }

        static class Es
        {
            const int NumVal = 5;
            static int buffer;
            static SemaphoreSlim bufferVuoto = new SemaphoreSlim(1);
            static SemaphoreSlim bufferPieno = new SemaphoreSlim(0);
            public static void Metti()
            {
                for (int i = 0; i < NumVal; i++)
                {
                    bufferVuoto.Wait();
                    int numB = 0;
                    do
                    {
                        numB = new Random().Next(1, 200);
                    } while (numB % 2 != 0);
                    buffer = numB;
                    Console.WriteLine($"Ho messo {buffer}");
                    bufferPieno.Release();
                }
            }
            public static void Togli()
            {
                for (int i = 0; i < NumVal; i++)
                {
                    bufferPieno.Wait();
                    if(buffer!= 0)
                    {
                        Console.WriteLine($"Ho tolto {buffer}");
                        buffer = 0;
                    }
                    bufferVuoto.Release();  
                }
            }
        }
    }
}
