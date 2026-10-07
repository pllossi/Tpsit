using System.Runtime.InteropServices;

namespace Ecommerce
{
    /*
     * Un e-commerce di prodotti alimentari vende un solo prodotto "in promozione" (alla volta) in quantità illimitata. Un operatore entra periodicamente nel sito, sostituisce il prodotto in promozione con uno nuovo in un tempo finito ed esce. Il cliente entra nel sito e compra, sempre in un tempo finito, il prodotto in promozione.
     * L'operatore può entrare nel sito, per aggiornare il prodotto, solo se non ci sono dei clienti all'interno.
     * I clienti possono entrare nel sito per comprare il prodotto solo se non c'è l'operatore all'interno e solo fino ad un massimo di N (numero randomico <= "numero registro" + 10) clienti nello stesso momento.
     * L'operatore sceglie il prodotto da mettere in promozione da un array finito dei prodotti vendibili.
     * Operatore e clienti accedono quindi in concorrenza al prodotto con thread separati.
     * Ad ogni modifica da parte dell'operatore, quest'ultimo non può rimettere in promozione il prodotto attuale.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            int numProdotti;
            do
            {
                Console.WriteLine("Inserire il numero di prodotti vendibili, minimo 2"); //Prendo in input il numero di prodotti vendibili
                var input = Console.ReadLine(); //Prendo in input il numero di prodotti vendibili
                int.TryParse(input, out numProdotti); //Converto l'input in intero
            } while (numProdotti < 2); //Controllo che il numero di prodotti vendibili sia maggiore di 1
            Parallel.Invoke(()=>Users.Operator(numProdotti), Users.Customer); //Eseguo in parallelo l'operatore e i clienti

        }
    }

    static public class Users
    {
        static int N = new Random().Next(1, 12); // Numero massimo di clienti che possono entrare nel sito nello stesso momento (randomico tra 1 e 11)
        const int numCicli = 15; // Numero di cicli di operazioni che l'operatore e i clienti devono eseguire
        static SemaphoreSlim accessoSitoAdmin = new SemaphoreSlim(1); // Semaforo per l'accesso al sito da parte dell'operatore (1 significa che può entrare solo 1 operatore alla volta)
        static SemaphoreSlim accessoSitoUser = new SemaphoreSlim(0); // Semaforo per l'accesso al sito da parte dei clienti (0 significa che non possono entrare finché l'operatore non esce)
        static List<string> prodottiVendibili = new List<string>(); // Lista dei prodotti vendibili
        static string prodottoInPromozione; // Prodotto attualmente in promozione
        /// <summary>
        /// Metodo che simula l'operatore che entra nel sito, sostituisce il prodotto in promozione con uno nuovo e esce. 
        /// L'operatore può entrare solo se non ci sono clienti all'interno.
        /// </summary>
        /// <param name="prod"></param>
        static public void Operator(int prod)
        {
            for (int i = 0; i < prod; i++)
            {
                prodottiVendibili.Add($"Prodotto {i + 1}"); // Aggiungo i prodotti vendibili alla lista
            }
            for (int i = 0; i < numCicli; i++)
            {
                {
                    accessoSitoAdmin.Wait(); // L'operatore entra nel sito (semaforo per l'accesso al sito da parte dell'operatore)
                    string nuovoProdotto; // Variabile per il nuovo prodotto da mettere in promozione
                    do
                    {
                        nuovoProdotto = prodottiVendibili[new Random().Next(prodottiVendibili.Count)]; // Scelgo un nuovo prodotto casuale dalla lista dei prodotti vendibili
                        if (nuovoProdotto == prodottoInPromozione)
                        {
                            Console.WriteLine($"Operatore ha scelto lo stesso prodotto in promozione: {nuovoProdotto}, scelgo un altro prodotto..."); // Stampo un messaggio se l'operatore ha scelto lo stesso prodotto in promozione
                        }
                    } while (nuovoProdotto == prodottoInPromozione); // Non posso mettere in promozione lo stesso prodotto attualmente in promozione
                    prodottoInPromozione = nuovoProdotto; // Aggiorno il prodotto in promozione
                    Console.WriteLine($"Operatore ha messo in promozione: {prodottoInPromozione}"); // Stampo il nuovo prodotto in promozione
                    accessoSitoUser.Release(); // L'operatore esce dal sito e permette ai clienti di entrare (semaforo per l'accesso al sito da parte dei clienti)

                }
            }
        }
        /// <summary>
        /// Permetto ai clienti di entrare nel sito e comprare il prodotto in promozione. 
        /// I clienti possono entrare solo se non c'è l'operatore all'interno e solo fino ad un massimo di N clienti nello stesso momento.
        /// </summary>
        static public void Customer()
        {
            for (int i = 0; i < numCicli; i++) // Ciclo per permettere ai clienti di entrare nel sito e comprare il prodotto in promozione
            {
                accessoSitoUser.Wait(); // I clienti entrano nel sito (semaforo per l'accesso al sito da parte dei clienti)
                for (int j = 0; j < N; j++) // Ciclo per permettere a N clienti di comprare il prodotto in promozione
                {
                    Console.WriteLine($"Cliente {j + 1} ha comprato: {prodottoInPromozione}"); // Stampo il prodotto comprato dal cliente
                }
                accessoSitoAdmin.Release(); // I clienti escono dal sito e permettono all'operatore di entrare (semaforo per l'accesso al sito da parte dell'operatore)
            }
        }
    }
}
