using System;
using System.Threading;
using System.Threading.Tasks;

const int NumVal = 10;
static int buffer;
static SemaphoreSlim bufferVuoto = new SemaphoreSlim(1);
static SemaphoreSlim bufferPieno = new SemaphoreSlim(0);

static void Main(string[] args)
{
    Parallel.Invoke();
}

