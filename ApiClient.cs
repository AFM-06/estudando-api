namespace APIandre;
using System.Text.Json;

public class ApiClient
{
    public async Task<Model?> GetPostAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://jsonplaceholder.typicode.com/posts/1");
        using var client = new HttpClient();

        HttpResponseMessage response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Model>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if(model != null)
            {
                Console.WriteLine($"Sucesso na requisição: {response.StatusCode}");
                return model;
            }
            else
            {
                Console.WriteLine($"Erro na requisição: {response.StatusCode}");
            }

        }
        return null;
    }
}