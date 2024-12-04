using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace SimpleChat;

public class ClientHelper
{
    private TcpClient _client;
    private NetworkStream _stream;

    public ClientHelper(TcpClient client)
    {
        _client = client;
        _stream = client.GetStream();
    }

    public bool IsMessageAvailable()
    {
        return _stream.DataAvailable;
    }

    public string ReceiveMessage()
    {
        StringBuilder message = new StringBuilder();
        
        int bytesRead;
        byte[] buffer = new byte[256];

        try
        {
            while (_stream.DataAvailable)
            {
                bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                message.Append(msg);
            }
        }
        catch
        {
            Console.WriteLine("Connection lost");
            Close();
            return string.Empty;
        }
        
        
        return message.ToString();
    }

    public void SendMessage(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }
        catch
        {
            Console.WriteLine($"Connection lost");
            Close();
        }
    }

    public bool IsConnected()
    {
        try
        {
            bool part1 = _client.Client.Poll(1000, SelectMode.SelectRead);
            bool part2 = _client.Client.Available == 0;
            return !part1 || !part2;
        }
        catch
        {
            return false;
        }
    }

    public void Close()
    {
        _stream?.Close();
        _client?.Close();
    }
}