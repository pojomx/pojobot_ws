using WebSocketSharp;
using WebSocketSharp.Server;

namespace csharp_server
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received message from client: " + e.Data);
            Send(e.Data);
        }
    }

    public class EchoAll : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Received message from client: " + e.Data);
            Sessions.Broadcast(e.Data);
        }
    }

    internal class Program
    { 
        static void Main(string[] args)
        {
            string serverIP = "ws://192.168.1.151:5355";
            WebSocketServer server = new WebSocketServer(serverIP);

            server.AddWebSocketService<Echo>("/Echo");
            server.AddWebSocketService<Echo>("/EchoAll");


            server.Start();
            Console.WriteLine("Server started at: " + serverIP);
            Console.ReadLine();

            server.Stop();
            Console.WriteLine("Server stopped");
        }
    }
}