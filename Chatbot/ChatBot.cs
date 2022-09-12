using WebSocketSharp;

namespace Chatbot
{
    public class Log
    {
        static public void i(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class ChatBot
    {
        private string ip = "irc-ws.chat.twitch.tv";
        private int port = 443;

        private string password = "";
        private string username = "pojoteca";
        private string canal = "pojomx";

        private string chatFormat 
        {
            get{
                return $"[{DateTime.Now.ToShortTimeString()}][{this.username}@{this.canal}] >> ";
            }
        } 

        public ChatBot(string username, string password, string canal)
        {
            this.password = password;
            this.username = username;
            this.canal = canal;
        }   

        public void Start()
        {
            //Create an instnace of a websocket client.
            string serverIP = $"wss://{ip}:{port}/";
            Console.WriteLine($"Conectando a {serverIP}:{port}");

            using (WebSocket ws = new WebSocket(serverIP))
            {
                ws.OnMessage += (object? sender, MessageEventArgs e) =>
                {
                    var line = e.Data.Trim();
                    Console.WriteLine($"\r[{DateTime.Now.ToShortTimeString()}]>>{e.Data}");

                    var split = line.Split(" ");
                    if (line.StartsWith("PING"))
                    {
                        if (split[1] != null && sender != null)
                        {
                            ws.Send($"PONG {split[1]}");
                        }
                    }
                    else
                    {
                        TwitchChatMessage message = parse_message(ws, line);
                    }

                    Console.Write($"{chatFormat} ");
                };

                ws.EmitOnPing = true;

                ws.Connect();
                Console.WriteLine("Conectado");
                Console.WriteLine($"{username} entrando a #{canal}");
                ws.Send($"PASS {password}");
                ws.Send($"NICK {username}");
                ws.Send($"JOIN #{canal}");

                Console.WriteLine($"{username} entrando a #{canal}");

                string entrada = "";
                bool terminar = false;

                do
                {
                    Console.Write($"{chatFormat} ");
                    entrada = Console.ReadLine();

                    switch (entrada)
                    {
                        case "bye":
                            terminar = true;
                            break;
                        default:

                            if(!parse_comando(ws, entrada))
                            {
                                ws.Send($"PRIVMSG #{canal} :{entrada}");
                            }
                            break;
                    }
                } while (!terminar);
                ws.Close();
            }
        }

        private void process_message_commands(WebSocket ws, string entrada)
        {
            TwitchChatMessage mensaje = new TwitchChatMessage(entrada);
            if (mensaje.channel == this.canal)
            {
                if (entrada.StartsWith('!')) {
                    
                    var split = entrada.Split(' ');
                    var comando = split[0].Substring(1);

                    if(!parse_comando(ws, comando))
                    {
                        send(ws, comando);
                    }
                }
            }
        }

        private bool parse_comando(WebSocket ws, string comando)
        {
            switch (comando)
            {
                case "switch":
                    send(ws, $"Este es el friend code de pojomx: 2750-1172-3867");
                    return true;
                case "discord":
                    send(ws, "Únete al discord del canal, utilizando el siguiente enlace: https://discord.gg/JCBSA55TCX");
                    return true;
                default:
                    return false;
            }
        }

        private void send(WebSocket ws, string mensaje)
        {
            var entrada = new TwitchChatMessage(DateTime.Now, canal, "PRIVMSG", username, mensaje);
            send(ws, entrada);
        }

        private void send(WebSocket ws, TwitchChatMessage mensaje) 
        {
            ws.Send($"PRIVMSG #{mensaje.channel} :{mensaje.message}");
        }

        private TwitchChatMessage parse_message(WebSocket ws, string entrada)
        {
            TwitchChatMessage mensaje = new TwitchChatMessage(entrada);

            if (mensaje.channel == this.canal)
            {
                Console.WriteLine($"[{mensaje.date.ToShortTimeString()}][{mensaje.user}@{mensaje.channel}] >> {mensaje.message}");
                Console.Write($"{chatFormat} ");
            }

            return mensaje;
        }
    }
}