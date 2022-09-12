using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    internal class Coordinador
    {
        private static ChatBot? chatbot = null;

        private List<Task> modulos;

        public Coordinador()
        {
            string pass = File.ReadAllText("C:\\secure\\pojoteca.pass.txt");

            chatbot = new ChatBot("pojoteca", pass, "pojomx");

            // Inicializa
            modulos = new List<Task>();
        }

        public async Task Start()
        {
            if(chatbot == null)
            {
                Console.WriteLine("Error cargando al chatbot.");
                return;
            }

            //if (botonera != null) modulos.Add(botonera.Start());

            try
            {
                //Task.Wait(modulos.ToArray());

                chatbot.Start();
            }
            catch (AggregateException e)
            {
                Log.i("The following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Log.i("\n-------------------------------------------------\n");
                    Log.i(e.InnerExceptions[j].ToString());
                }
            }
        }
    }
}
