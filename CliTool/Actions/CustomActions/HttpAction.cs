using System.Text;

namespace CliTool.Actions.CustomActions;

public class HttpAction : BaseAction
{
    public string? Method { get; set; }
    public string? Url { get; set; }
    public string? Body { get; set; }

    private static readonly HttpClient HttpClient = new();

    public override async Task<bool> Act()
    {
        if (string.IsNullOrWhiteSpace(Method) || string.IsNullOrWhiteSpace(Url))
        {
            Console.WriteLine("HttpAction: Method or Url is missing.");
            return false;
        }

        try
        {
            HttpRequestMessage request = new(new HttpMethod(Method), Url);

            if (!string.IsNullOrEmpty(Body))
            {
                request.Content = new StringContent(Body, Encoding.UTF8, "application/json");
            }

            var response = await HttpClient.SendAsync(request);

            Console.WriteLine($"HttpAction: {Method} {Url} returned {(int)response.StatusCode}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"HttpAction: Exception occurred - {ex.Message}");
            return false;
        }
    }
}