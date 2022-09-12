namespace Chatbot
{
    internal class Program
    {
        static async Task Main()
        {
            var coordinador = new Coordinador();
            await coordinador.Start();
        }
    }
}