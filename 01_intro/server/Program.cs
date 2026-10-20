using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    static string address = "127.0.0.1";
    static int port = 8080; // 1000 .... 60 000
    private static void Main(string[] args)
    {

        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        //Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient listener = new UdpClient(ipPoint);
        try
        {
            //listener.Bind(ipPoint);
            Console.WriteLine("Server started!!! Waiting for connection");

            while (true)
            {
                //byte[] data = new byte[1024];
                //bytes = listener.ReceiveFrom(data, ref remoteEndPoint);

                byte[] data = listener.Receive(ref remoteEndPoint); 

                string message = Encoding.Unicode.GetString(data);
                Console.WriteLine($"{DateTime.Now.ToShortDateString()}[{DateTime.Now.ToShortTimeString()}] : {message} from {remoteEndPoint}");

                // send answer
                message = "Message was send!!!";
                data = Encoding.Unicode.GetBytes(message);
                listener.Send(data,data.Length, remoteEndPoint);
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        //listener.Shutdown(SocketShutdown.Both);
        listener.Close();
    }
}