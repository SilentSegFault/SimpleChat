using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat;

public class Server
{
    private TcpListener tcpListener;
    private IPEndPoint endPoint;
    private List<ClientHelper> clients;
    private bool IsRunning = false;
    
    public Server(IPAddress address, int port)
    {
       endPoint = new IPEndPoint(address, port);
       tcpListener = new TcpListener(address, port);
       clients = new List<ClientHelper>();
    }

    public void Start()
    {
        tcpListener.Start();
        Console.WriteLine("Server started");
        IsRunning = true;

        Thread clientAcceptThread = new Thread(ClientAcceptThread);
        Thread clientMessageThread = new Thread(ClientMessageThread);
        
        clientAcceptThread.Start();
        clientMessageThread.Start();
    }

    private void ClientAcceptThread()
    {
        while (IsRunning)
        {
            if (!tcpListener.Pending())
            {
                Thread.Sleep(250);
                continue;
            }
            
            TcpClient client = tcpListener.AcceptTcpClient();
            Console.WriteLine("Client connected");
            ClientHelper clientHelper = new ClientHelper(client);
            clientHelper.SendMessage("Welcome to the server!\r\n");
            clients.Add(clientHelper);
        }
    }

    private void ClientMessageThread()
    {
        List<KeyValuePair<ClientHelper, string>> messages = new List<KeyValuePair<ClientHelper, string>>();
        while (IsRunning)
        {
            foreach (ClientHelper client in clients.ToList())
            {
                if (!client.IsConnected())
                {
                    Console.WriteLine("Client disconnected");
                    messages.Add(new KeyValuePair<ClientHelper, string>(client, "<SERVER> Client disconnected"));
                    clients.Remove(client);
                    continue;
                }
                
                if (client.IsMessageAvailable())
                {
                    string msg = client.ReceiveMessage();
                    Console.WriteLine($"[msg]: {msg}");
                    messages.Add(new KeyValuePair<ClientHelper, string>(client, msg)); 
                }
            }

            foreach (ClientHelper client in clients.ToList())
            {
                foreach (KeyValuePair<ClientHelper, string> msg in messages)
                {
                    if(client.IsConnected() && msg.Key != client)
                        client.SendMessage(msg.Value);
                }
            }
            
            messages.Clear();
            
            Thread.Sleep(100);
        }
    }

    public void Stop()
    {
        IsRunning = false;
        tcpListener.Stop();
        Console.WriteLine("Server stopped");
    }
}