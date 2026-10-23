using System.Net.Sockets;
using System.Net;
using System.Text;

public class ChatServer
{
    const int port = 4040;
    const string JOIN_CMD = "$<Join>";

    TcpListener server;
    IPEndPoint address = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
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

            if(message == "$<close>")
            {
                ns.Close();
                server?.Stop();
                break;
            }

            Console.WriteLine($"Got message {message} from {client.Client.LocalEndPoint} {DateTime.Now.ToShortTimeString()}");
            sw.WriteLine(message);
            sw.Flush();
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