using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

using System.IO;

using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat;

public class Client
{
    private ClientHelper client;

    public bool Connect(IPAddress ip, int port)
    {
        TcpClient tcpClient = new TcpClient();
        tcpClient.Connect(ip, port);
        client = new ClientHelper(tcpClient);

        Thread messageReceivedThread = new Thread(MessageReceiveThread);
        messageReceivedThread.Start();
        
        return true;
    }

    private void MessageReceiveThread()
    {
        while (client.IsConnected())
        {
            if (client.IsMessageAvailable())
            {
                string msg = ReceiveMessage(false);
                Console.WriteLine($"[msg]: {msg}");
            }
        }
    }

    public string ReceiveMessage(bool wait)
    {
        while (wait && !client.IsMessageAvailable())
            ;
        return client.ReceiveMessage();
    }

    public void SendMessage(string message)
    {
        client.SendMessage(message);
    }

    public bool Disconnect()
    {
        client.Close();
        return true; 
    }

    public bool IsConnected()
    {
        return client.IsConnected();
    }
}