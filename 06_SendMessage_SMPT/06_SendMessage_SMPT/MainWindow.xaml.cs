using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
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

namespace _06_SendMessage_SMPT;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    string server = "smtp.gmail.com";
    int port = 587;

    string username = @"dashakonopelko@gmail.com";
    string password = "q u m x q g a w i s w y a l p x";
    string otheruser = "horeki8069@37xiao.com";

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Click_MessageSend(object sender, RoutedEventArgs e)
    {
        MailMessage message = new MailMessage(username, otheruser, "Theme", "Hello World");
        using(StreamReader sr = new StreamReader(@"D:\Dasha\Programming\Gropus\HTML-CSS_PV511\01_helper_home\index.html"))
        {
            message.Body += sr.ReadToEnd();
        }
        message.IsBodyHtml = true;

        message.Priority = MailPriority.High;
        message.Attachments.Add(new Attachment(@"D:\Dasha\Programming\Gropus\HTML-CSS_PV511\04_image\img\2.png"));
        message.Attachments.Add(new Attachment(@"C:\Users\kap19\Downloads\TIC_TAC_TOE- TASK.txt"));


        SmtpClient client = new SmtpClient(server,port);

        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(username, password);
        client.SendCompleted += Client_sendCompleted;

        client.SendAsync(message, message);
    }
    private void Client_sendCompleted(object sender, AsyncCompletedEventArgs e)
    {
        var state = (MailMessage)e.UserState!;
        MessageBox.Show($"Message was send! Subject : {state?.Subject}");
    }
}