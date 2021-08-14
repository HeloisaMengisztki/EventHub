using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Thread thread = new Thread(new ThreadStart(DoSomething));
            // thread.Name = "TH1";
            // thread.Start();
            //
            // Thread thread2 = new Thread(new ThreadStart(DoSomething));
            // thread2.Name = "TH2";
            // thread2.Start();

            await DoSomething("A");
            
            await DoSomething("B");

            Console.WriteLine("FIM Hello World!");

            Console.ReadLine();
        }

        public static async Task DoSomething(string? nome)
        {
            var threadName = nome;

            var x = new Consumidor();
            await x.Consumir(threadName);
        }
    }
}