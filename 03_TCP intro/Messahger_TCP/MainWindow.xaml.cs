using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Messahger_TCP;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    const string serverAddress = "127.0.0.1";
    const int serverPort = 4040;
    IPEndPoint serverPoint;
    TcpClient client = null;
    ObservableCollection<MessageInfo> messages;

    NetworkStream ns = null;
    StreamReader sr = null;
    StreamWriter sw = null;
    
    public MainWindow()
    {
        InitializeComponent();
        
        serverPoint = new IPEndPoint(IPAddress.Parse(serverAddress), serverPort);
        messages = new ObservableCollection<MessageInfo>();
        this.DataContext = messages;
    }

    private void SendButtonClick(object sender, RoutedEventArgs e)
    {

        string message = msgTextBox.Text;
        msgTextBox.Text = "";
        if (sw == null)
            return;
        sw.WriteLine(message);
        sw.Flush();
    }
    private void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            client = new TcpClient();
            client.Connect(serverPoint);
            ns = client.GetStream();

            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message);
        }

    }

    private void DisconnectButton_Click(object sender, RoutedEventArgs e)
    {
        
    }

    private  void Listener()
    {
        
    }

}
public class MessageInfo
{
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public MessageInfo(string message)
    {
        Text = message;
        Time = DateTime.Now;
    }
    public override string ToString()
    {
        return $"{Text,-20} {Time}";
    }
}
