using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    internal class TwitchChatMessage
    {
        public DateTime date;
        public string channel;
        public string type;
        public string user;
        public string message;

        public TwitchChatMessage(DateTime date, string channel, string type, string user, string message)
        {
            this.date = date;
            this.channel = channel;
            this.type = type;
            this.user = user;
            this.message = message;
        }   

        public TwitchChatMessage(string entrada)
        {
            // Tipos de entradas
            //      :pojomx!pojomx@pojomx.tmi.twitch.tv PRIVMSG #pojomx :hola
            
            string[] split = entrada.Split(' ');
            // Split 0 => :user!channel@channel.tmi.twitch.tv
            // Split 1 => PRIVMSG
            // Split 2 => #channel
            // Split 3+ => message

            // Separar Split 0 para sacar usuario y canal
            int exclamationPointPosition = split[0].IndexOf("!");
            // Sacar el segundo doble punto para saber donde comienza el mensaje
            int secondColonPosition = entrada.IndexOf(':', 1);
            this.date = DateTime.Now;

            try
            {
                this.user = split[0].Substring(1, exclamationPointPosition - 1);
                this.channel = split[0].Substring(exclamationPointPosition + 1);
                this.type = split[1];
                this.message = entrada.Substring(secondColonPosition + 1);    
            } 
            catch(ArgumentOutOfRangeException ex)
            {
                this.user = "";
                this.channel = split[0];
                this.type = split[1];
                this.message = entrada.Trim();
            }
        }
    }
}
