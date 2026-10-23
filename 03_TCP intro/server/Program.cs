using System.Net.Sockets;
using System.Net;
using System.Text;

public class ChatServer
{
    const int port = 4040;
    const string JOIN_CMD = "$<Join>";
    
    TcpListener server;
    IPEndPoint address = new IPEndPoint(IPAddress.Parse("10.7.0.0.1"),port);
    public ChatServer()
    {
        server = new TcpListener(address);
    }

    public void Start()
    {
        server.Start();
        Console.WriteLine("Waiting for connection");
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("Connected!!!");
        NetworkStream ns = client.GetStream();
        StreamReader sr = new StreamReader(ns);
        StreamWriter sw = new StreamWriter(ns);

        while (true)
        {
            string message = sr.ReadLine()!;
            Console.WriteLine($"Got message {message} from {client.Client.LocalEndPoint} {DateTime.Now.ToShortTimeString()}");
        }
    }
    
}
internal class Program
{
    private static void Main(string[] args)
    {
        ChatServer chat = new ChatServer();
        chat.Start();
    }

}