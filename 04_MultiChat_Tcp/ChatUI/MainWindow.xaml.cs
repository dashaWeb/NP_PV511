using ClientRequest;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string remoteIP = "127.0.0.1";
    private int port = 4040;
    TcpClient client = new TcpClient();
    ObservableCollection<string> messages;
    public MainWindow()
    {
        InitializeComponent();
        messages = new ObservableCollection<string>();
        this.DataContext = messages;
    }

    private async void JoinButton_Click(object sender, RoutedEventArgs e)
    {
        if (client.Connected) return;

        await client.ConnectAsync(IPAddress.Parse(remoteIP), port);
        SendRequest(new ClientCommand(RequestCommand.Join));

        ServerListener();
        
    }
    private async void ServerListener()
    {
        while (true)
        {
            ClientCommand command = await GetResponse();
            if(command.Command == RequestCommand.Message)
            {
                messages.Add(command.Message);
            }
        }
    }
    private Task<ClientCommand> GetResponse()
    {
        return Task.Run(() =>
        {
            NetworkStream ns = client.GetStream();
            StreamReader sr = new StreamReader(ns);
            string resp = sr.ReadLine()!;

            return JsonSerializer.Deserialize<ClientCommand>(resp)!;
        });
    }
    private async void SendRequest(ClientCommand command)
    {
        NetworkStream ns = client.GetStream();

        StreamWriter sw = new StreamWriter(ns);
        string json = JsonSerializer.Serialize<ClientCommand>(command);
        
        await sw.WriteLineAsync(json);
        await sw.FlushAsync();
    }

    private void LeaveButton_Click(object sender, RoutedEventArgs e)
    {
        SendRequest(new ClientCommand(RequestCommand.Leave));
    }

    private void SendButtonClick(object sender, RoutedEventArgs e)
    {
        SendRequest(new ClientCommand(msgTextBox.Text));
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        MessageBox.Show("Close");
    }
}