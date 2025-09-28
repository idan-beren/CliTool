using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.Logging;

namespace CliTool.Actions.CustomActions;

public class HttpAction : BaseAction
{
    [Required(ErrorMessage = "Method is required")]
    public string? Method { get; set; }
    
    [Required(ErrorMessage = "Url is required")]
    public string? Url { get; set; }
    
    public string? Body { get; set; }

    private static readonly HttpClient HttpClient = new();

    public override async Task<bool> Act()
    {
        try
        {
            var response = await GetHttpResponse();
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Http exception: {Message}", ex.Message);
            return false;
        }
    }

    private async Task<HttpResponseMessage> GetHttpResponse()
    {
        HttpRequestMessage request = new(new HttpMethod(Method!), Url);

        if (!string.IsNullOrEmpty(Body))
        {
            request.Content = new StringContent(Body, Encoding.UTF8, "application/json");
        }

        var response = await HttpClient.SendAsync(request);
        Logger.LogInformation("Http request - {Method} {Url} got response: {Response}", Method, Url, response.StatusCode);
        return response;
    }
}