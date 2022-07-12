using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ConsolaNotificaciones
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundWorker tarea = new BackgroundWorker();
            tarea.DoWork += HacerAlgo;
            tarea.RunWorkerCompleted += Terminado;
            tarea.RunWorkerAsync();
            Console.ReadLine();
        }

        static void HacerAlgo(object o, DoWorkEventArgs e)
        {
            Console.WriteLine("iniciando trabajo");
            System.Threading.Thread.Sleep(4000);
        }

        static void Terminado(object o, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("iniciando trabajo");
        }

    }
}
