namespace APIandre;
public class Program
{
    static async Task Main(string[] args)
    {
        var service = new ApiClient();
        Model post = await service.GetPostAsync();

        Console.WriteLine($"Titulo 1: {post.Title}");
    }
}