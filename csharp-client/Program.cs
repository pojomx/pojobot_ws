using WebSocketSharp;

namespace csharp_client
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var coordinador = new Coordinador();
            await coordinador.Start();

            //Create an instnace of a websocket client.

            string serverIP = "ws://192.168.1.151:5355/Echo";
         

            Console.WriteLine("Conectando a " + serverIP);

            using (WebSocket ws = new WebSocket(serverIP))
            {
                ws.OnMessage += Ws_OnMessage;
                ws.Connect();
                Console.WriteLine("Conectado");

                Console.WriteLine("Presione una tecla para terminar");
                Console.ReadKey();
                ws.Close();
            }
        }

        private static void Ws_OnMessage(object? sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
        }
    }
}