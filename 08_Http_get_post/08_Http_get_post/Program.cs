using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
internal class Program
{
    static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    private static async Task Main(string[] args)
    {
        // GET Method

        string url = @"https://jsonplaceholder.typicode.com/posts";
        //string url = @"https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";
        //using (var client = new HttpClient())
        //{
        //    var response = await client.GetAsync(url);

        //    Console.WriteLine($"Status :: {response.StatusCode}");
        //    var result = await response.Content.ReadAsStringAsync();

        //    Console.WriteLine(result);
        //}

        //var post = new Post
        //{
        //    Title = "NewTitle",
        //    Body = "NewBody",
        //    UserId = 1
        //};
        //// POST Method
        //var json = JsonConvert.SerializeObject(post);
        //var data = new StringContent(json, Encoding.UTF8, "application/json");

        //using(var client = new HttpClient())
        //{
        //    var response = await client.PostAsync(url, data);
        //    Console.WriteLine($"Status :: {response.StatusCode}");
        //    var result = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(result);
        //}


        
        string urlImg = @"https://i.pinimg.com/736x/ef/5b/ff/ef5bffb9febf471cc0b2e495e12bf509.jpg";

        // way 1
        //HttpClient client = new HttpClient();
        //HttpRequestMessage message = new HttpRequestMessage()
        //{
        //    Method = HttpMethod.Get,
        //    RequestUri = new Uri(urlImg)
        //};

        //var response = await client.SendAsync(message);
        //Console.WriteLine($"Status :: {response.StatusCode}");

        //using (FileStream fs = new FileStream(Path.Combine(desktopPath, Path.GetFileName(urlImg)), FileMode.Create))
        //{
        //    await response.Content.CopyToAsync(fs);
        //}

        //way 2
        //var img2 = @"https://dettext.com/wp-content/uploads/2024/02/krasivye-peizazhi-dlia-srisovki/031991-dettext_com-krasivye-peizazhi-dlia-srisovki.jpg";
        //HttpClient client = new HttpClient();
        //byte[] data = await client.GetByteArrayAsync(img2);
        //File.WriteAllBytes(Path.Combine(desktopPath, Path.GetFileName(img2)), data);


        //WebClient client = new WebClient();
        //client.DownloadFile(new Uri(@"https://cdn.27.ua/799/54/f6/2839798_2.jpeg"),desktopPath + "/2.jpeg");
        //Console.WriteLine("File loaded");

        // async loaded
        DownloadFileAsync(@"https://i.pinimg.com/736x/ef/5b/ff/ef5bffb9febf471cc0b2e495e12bf509.jpg");
    }

    private static async void DownloadFileAsync(string address)
    {
        WebClient client = new WebClient();

        client.DownloadFileCompleted += Client_DownloadComplated;
        client.DownloadProgressChanged += Client_DownloadProgressChanged;

        string fileName = Path.Combine(desktopPath, Path.GetFileName(address));

        await client.DownloadFileTaskAsync(address, fileName);

        Console.WriteLine("File loaded");

    }
    private static void Client_DownloadComplated(object? sender, AsyncCompletedEventArgs e)
    {
        if(e.Cancelled)
            Console.WriteLine("File downloading was cancelled");
        else
            Console.WriteLine("File downloaded succesfully!");
    }
    private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        Console.WriteLine($"Downloading .... \t {(float)e.BytesReceived / 1024 / 1024}Mb of {(float)e.TotalBytesToReceive / 1024 / 1024}Mb \t {e.ProgressPercentage}%");
    }
}