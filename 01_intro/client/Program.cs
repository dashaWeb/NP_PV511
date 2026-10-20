using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{

    static string address = "127.0.0.1";
    static int port = 8080; // 1000 .... 60 000
    private static void Main(string[] args)
    {

        try
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port); // server
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0); // server
            string message = "";
            UdpClient client = new UdpClient();
            while (message != "end")
            {
                Console.WriteLine("Enter a message : ");
                message = Console.ReadLine()!;

                byte[] data = Encoding.Unicode.GetBytes(message);
                //socket.Connect(ipPoint);
                //socket.Send(data);  
                //socket.SendTo(data, ipPoint);
                client.Send(data, data.Length, ipPoint);

                //int bytes = 0;
                //string response = "";
                //data = new byte[1024];

                //do
                //{
                //    bytes = socket.ReceiveFrom(data,ref remoteEndPoint);
                //    response += Encoding.Unicode.GetString(data, 0, bytes);
                //} while (socket.Available > 0);

                data = client.Receive(ref remoteEndPoint);
                string response = Encoding.Unicode.GetString(data);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\t\t Server response : {response} -- {DateTime.Now.ToShortDateString()}[{DateTime.Now.ToShortTimeString()}]");
                Console.ResetColor();
            }
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Close();
            client.Close();
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
    }
}