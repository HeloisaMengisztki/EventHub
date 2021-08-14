using System;
using System.Threading.Tasks;

namespace Publicador
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("Iniciando Publicação de eventos");
            Console.WriteLine("");

            var parar = true;

            while (parar)
            {
                var qtdeEventos = 999;
                //var qtdeEventos = int.Parse(Console.ReadLine());

                if (qtdeEventos > 1000)
                {
                    Console.WriteLine($"Quandidade de mensagens superior {qtdeEventos} à 100");
                    return;
                }

                Publicar(qtdeEventos).GetAwaiter().GetResult(); 

                Console.WriteLine("Deseja parar o processo? [S/N]");
                if (Console.ReadLine().ToLower().StartsWith("s"))
                {
                    parar = false;
                };
            }
        }

        static async Task Publicar(int qtdeEventos)
        {
            var publicador = new Publicador();
            await publicador.Publicar(qtdeEventos);            
        }
    }
}