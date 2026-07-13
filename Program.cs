namespace APIandre;
public class Program
{
    static async Task Main(string[] args)
    {
        var service = new ApiClient();
        Model? post = await service.GetPostAsync();

    if (post is null)
    {
        Console.WriteLine("No post found.");
        return;
    }

    Console.WriteLine($"Titulo: {post.Title}");
    }
}