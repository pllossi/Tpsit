using System.Threading;

namespace Mercato
{
    public class MercatoStato
    {
        private int _isOpen = 1;

        public bool IsOpen => Interlocked.CompareExchange(ref _isOpen, 1, 1) == 1;

        public void Close() => Interlocked.Exchange(ref _isOpen, 0);

        public void Open() => Interlocked.Exchange(ref _isOpen, 1);
    }
}