namespace APIandre;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
public class ApiClient
{
    private readonly string _baseUrl = "https://jsonplaceholder.typicode.com/posts";
    private static readonly HttpClient _client = new HttpClient();
    public async Task GetAsync()
    {
        Console.WriteLine("-----------------------------");

        var request = new HttpRequestMessage(HttpMethod.Get,_baseUrl);

        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var models = JsonSerializer.Deserialize<List<Model>>(json,new JsonSerializerOptions { PropertyNameCaseInsensitive = true })?? new List<Model>();
            Console.WriteLine($"Sucesso na requisição: {response.StatusCode}");
            foreach(var model in models)
            {
                Console.WriteLine($"UserId: {model?.UserId}");
                Console.WriteLine($"Id: {model?.Id}");
                Console.WriteLine($"Title: {model?.Title}");
                Console.WriteLine($"Body: {model?.Body}");
            }
            Console.WriteLine("----------------------");
        }else
        {
            Console.WriteLine($"Erro na requisição: {response.StatusCode}");
        }
        Console.WriteLine("-----------------------------");
    }
    public async Task<Model?> GetAsync_post_one()
    {
        Console.WriteLine("-----------------------------");
        var request = new HttpRequestMessage(HttpMethod.Get,$"{_baseUrl}/1");
        HttpResponseMessage response = await _client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Model>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            Console.WriteLine($"Sucesso na requisição: {response.StatusCode}");
            Console.WriteLine("-----------------------------");
            return model;
        }
        else
        {
            Console.WriteLine($"Erro na requisição: {response.StatusCode}");
            Console.WriteLine("-----------------------------");
            return null;
        }

    }
    public async Task PostAsync(Model post)
    {
        Console.WriteLine("-----------------------------");

        string json = JsonSerializer.Serialize(post);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl)
        {
            Content = content
        };

        HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Post feito ({response.StatusCode}) {responseBody}.");
        }
        else
        {
            Console.WriteLine($"Erro {response.StatusCode}");
        }
        Console.WriteLine("-----------------------------");
    }
    public async Task DeleteAsync(int delete)
    {
        Console.WriteLine("-----------------------------");

        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{delete}");
        HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"{response.StatusCode}");
            Console.WriteLine($"O Id {delete} foi deletado.");
        }
        else
        {
            Console.WriteLine($"Erro: {response.StatusCode}");
        }
        Console.WriteLine("-----------------------------");
    }
    public async Task PutAsync(int id, Model put)
    {
        Console.WriteLine("-----------------------------");

        string json = JsonSerializer.Serialize(put);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{id}")
        {
          Content = content
        };

        HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Codigo: {response.StatusCode} / {responseBody}");

        }
        else
        {
            Console.WriteLine($"Erro: {response.StatusCode}.");
        }
        Console.WriteLine("-----------------------------");
    }
    public async Task PatchAsync(int id, object atualizado)
    {
        Console.WriteLine("-----------------------------");
        string json = JsonSerializer.Serialize(atualizado);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Patch, $"{_baseUrl}/{id}")
        {
            Content = content
        };
        HttpResponseMessage response = await _client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Codigo: {response.StatusCode} / Id: {id} atualizado {responseBody}");
        }
        else
        {
            Console.WriteLine($"Erro: {response.StatusCode}.");
        }
        Console.WriteLine("-----------------------------");
    }
}