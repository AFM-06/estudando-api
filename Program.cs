namespace APIandre;
public class Program
{
    static async Task Main(string[] args)
    {
        var service = new ApiClient();
        Model? getOne = await service.GetAsync_post_one();
        Console.WriteLine(getOne?.Title);
        //await service.GetAsync();
        Model post = new()
        {
            UserId = 1,
            Id = 1,
            Title = "Testando Post",
            Body = "Testando Post"
        };
        await service.PostAsync(post);
        await service.DeleteAsync(2);
        Model put = new()
        {
            UserId = 1,
            Id = 1,
            Title = "Testando Put",
            Body = "Testando Put"
        };
        await service.PutAsync(1,put);
        await service.PatchAsync(1,new {id = 3});
    }
}
