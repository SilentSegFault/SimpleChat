using System.Net;

namespace SimpleChat;

class Program
{
    static void Main(string[] args)
    {
        string? option = Console.ReadLine();
        if (option == "server")
        {
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080);
            server.Start();
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "exit")
                {
                    server.Stop();
                    break;
                }
            }
        }
        else if (option == "client")
        {
            Client client = new Client();
            client.Connect(IPAddress.Parse("127.0.0.1"), 8080);
            while (client.IsConnected())
            {
                string msg = Console.ReadLine();
                if (msg == "exit")
                {
                    client.Disconnect();
                    break;
                }
                client.SendMessage(msg);
            }
        }
    }
}