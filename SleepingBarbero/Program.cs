using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarbero
{
    class Program
    {
        static string[] nombres = new string[] { "jorge", "turi", "ficachi", "pepe", "victor" };

        const int TODO = 3;
        static int AsientosLibres = TODO;

        //    static string[] buffer = new string[4];

        static Semaphore SBarbero, SEspera, SCliente;

        static void Main(string[] args)
        {
            Thread TBarbero = new Thread(Barbero);

            SBarbero = new Semaphore(1, 1);
            SEspera = new Semaphore(3, 3);
            SCliente = new Semaphore(0, 3);
            TBarbero.Start();


            Thread TCliente = new Thread(Cliente);
            Thread TCliente2 = new Thread(Cliente);
            Thread TCliente3 = new Thread(Cliente);
            Thread TCliente4 = new Thread(Cliente);
            Thread TCliente5 = new Thread(Cliente);
            TCliente.Start();
            TCliente2.Start();
            TCliente3.Start();
            TCliente4.Start();
            TCliente5.Start();


        }

        static private void Barbero()
        {
            while (true)
            {

                if (AsientosLibres == TODO)
                {
                    Console.WriteLine("El barbero se jue a dormir");                  
                    
                }
                else
                {
                    
                    AsientosLibres++;
                    Thread.Sleep(600);
                }


                SCliente.WaitOne();
            }
        }

        static private void Cliente()
        {
            while (true)
            {

                Random r = new Random();
                string cliente = nombres[r.Next(5)];



                BuscarAsiento(cliente);
                
                if (AsientosLibres > 0)
                {
                    SEspera.WaitOne();
                    
                    AsientosLibres--;
                    SCliente.Release();                 
                    
                    CortarCabello(cliente);
                    SEspera.Release();

                }
                else
                    Console.WriteLine("El cliente huyo");
                
                    


                Thread.Sleep(r.Next(500, 1000));
            }
        }

        private static void CortarCabello(string Nombre)
        {
            Console.WriteLine("El barbero esta cortando el cabello de " + Nombre + AsientosLibres);
        }
        private static void BuscarAsiento(string Nombre)
        {
            Console.WriteLine("Cliente: " + Nombre + ", busca cortarse El cabello");
        }
    }
}
