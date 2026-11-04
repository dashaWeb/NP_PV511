using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.Text;

internal class Program
{
    const string username = "dashakonopelko@gmail.com";
    const string password = "zzkv illw uwxl hfnv";
    const string otheruser = "fodehon720@boffweb.com";
    private static void Main(string[] args)
    {

        // send mails (SMTP)
        //MimeMessage message = new();
        //message.From.Add(new MailboxAddress("Dasha", username));
        //message.To.Add(new MailboxAddress("user", otheruser));

        //message.Subject = "Hello MailKit";
        //message.Importance = MessageImportance.High;

        //message.Body = new TextPart("SubTheme")
        //{
        //    Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        //            Proin tempor commodo neque, sit amet dapibus risus blandit vel. Sed quis urna eros. Duis eros tortor, pharetra vel fermentum sed, feugiat sed risus. 
        //            Sed ultrices, odio ac luctus fermentum, metus justo tempor nisi, vitae malesuada augue est quis leo. Nulla lacinia tempor tristique. 
        //            Praesent consectetur non ipsum a luctus. Etiam sodales, nulla quis volutpat bibendum, risus odio tincidunt massa, sed dapibus lorem lectus ut felis."
        //};


        //var body = new TextPart("SubTheme") {
        //    Text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        //            Proin tempor commodo neque, sit amet dapibus risus blandit vel. Sed quis urna eros. Duis eros tortor, pharetra vel fermentum sed, feugiat sed risus. 
        //            Sed ultrices, odio ac luctus fermentum, metus justo tempor nisi, vitae malesuada augue est quis leo. Nulla lacinia tempor tristique. 
        //            Praesent consectetur non ipsum a luctus. Etiam sodales, nulla quis volutpat bibendum, risus odio tincidunt massa, sed dapibus lorem lectus ut felis."
        //};

        //var path = @"C:\Users\kap19\Downloads\12.jpg";

        //var attachment = new MimePart("image","jpg")
        //{
        //    Content = new MimeContent(File.OpenRead(path), ContentEncoding.Default),
        //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        //    ContentTransferEncoding = ContentEncoding.Base64,
        //    FileName = Path.GetFileName(path)
        //};

        //var multi = new Multipart("mixed");
        //multi.Add(body);
        //multi.Add(attachment);

        //message.Body = multi;


        //var builder = new BodyBuilder();

        //// Set the plain-text version of the message text
        //builder.TextBody = @"Hey Alice,

        //            What are you up to this weekend? Monica is throwing one of her parties on
        //            Saturday and I was hoping you could make it.

        //            Will you be my +1?

        //            -- Joey
        //            ";

        //// In order to reference selfie.jpg from the html text, we'll need to add it
        //// to builder.LinkedResources and then use its Content-Id value in the img src.
        //var image = builder.LinkedResources.Add(@"C:\Users\kap19\Downloads\12.jpg");
        //image.ContentId = MimeUtils.GenerateMessageId();

        //// Set the html version of the message text
        //builder.HtmlBody = string.Format(@$"<p>Hey Alice,<br>
        //        <p>What are you up to this weekend? Monica is throwing one of her parties on
        //        Saturday and I was hoping you could make it.<br>
        //        <p>Will you be my +1?<br>
        //        <p>-- Joey<br>
        //        <center><img src='{image.ContentId}'></center>");

        //// We may also want to attach a calendar event for Monica's party...
        //builder.Attachments.Add(@"C:\Users\kap19\Downloads\TIC_TAC_TOE- TASK.txt");

        //// Now we just need to set the message body and we're done
        //message.Body = builder.ToMessageBody();

        //using (var client = new SmtpClient())
        //{
        //    client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
        //    client.Authenticate(username, password);
        //    client.Send(message);
        //}


        Console.OutputEncoding = Encoding.UTF8;

        using (var client = new ImapClient())
        {
            client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
            client.Authenticate(username, password);

            foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
            {
                Console.WriteLine(item.Name);
            }
            var top = client.GetFolder(client.PersonalNamespaces[0]);
            top.Create("New Folder", true);

            //client.GetFolder(MailKit.SpecialFolder.Sent).Open(MailKit.FolderAccess.ReadWrite);
            //var folder = client.GetFolder(MailKit.SpecialFolder.Sent);
            //var id = folder.Search(SearchQuery.All)[folder.Search(SearchQuery.All).Count - 1];
            //var message = folder.GetMessage(id);

            //Console.WriteLine($"{message.Date} {message.Subject}");

            //folder.MoveTo(id, client.GetFolder(MailKit.SpecialFolder.Trash));

            //folder.AddFlags(id, MessageFlags.Deleted, true);
            //folder.Expunge();

            //foreach (var item in folder.Search(SearchQuery.All))
            //{
            //    var res = folder.GetMessage(item);
            //    Console.WriteLine($"{res.Date} {res.Subject} \t {res?.Body?.ContentBase}" );
            //}

            //client.Inbox.Open(MailKit.FolderAccess.ReadWrite);
            //var inbox = client.Inbox.Search(SearchQuery.All);
            //foreach (var item in inbox)
            //{
            //    var res = client.Inbox.GetMessage(item);
            //    Console.WriteLine($"{res.Date} {res.Subject} ");
            //    foreach (var item1 in res.Attachments)
            //    {
            //        Console.WriteLine(item1?.ContentType.Name);
            //        Console.WriteLine(item1?.ContentDisposition?.FileName);
            //    }
            //}
        }
    }
}