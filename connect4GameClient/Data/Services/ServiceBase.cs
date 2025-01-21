using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

namespace connect4GameClient.Data.Services;

public class ServiceBase<ResourceType, ListType, SingleType>(HttpClient Client, string controller)
{
    private const string BASE_URL = "http://localhost:5034/api/";
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    private string Endpoint => $"{BASE_URL}{controller}";

    private static HttpRequestMessage MakeBasicRequest(HttpMethod method, string uri)
    {
        var request = new HttpRequestMessage(method, uri);

        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("User-Agent", "Connect4");
        request.Headers.Add("Access-Control-Allow-Origin", "*");

        return request;
    }

    public HttpRequestMessage MakePaginatedGetAllRequest(
        int limit,
        int offset,
        KeyValuePair<string, string>? filter = null)
    {
        var uri = filter.HasValue
        ? new Uri(QueryHelpers.AddQueryString(Endpoint, filter.Value.Key, filter.Value.Value)).ToString()
        : Endpoint;

        var request = MakeBasicRequest(
            HttpMethod.Get,
            uri);

        request.Headers.Add("limit", limit.ToString());
        request.Headers.Add("offset", offset.ToString());
        return request;
    }

    public HttpRequestMessage MakeGetOneRequest(int id)
        => MakeBasicRequest(
            HttpMethod.Get,
            $"{Endpoint}/{id}");

    public HttpRequestMessage MakeAddRequest(ResourceType data)
    {
        var request = MakeBasicRequest(
            HttpMethod.Post,
            $"{Endpoint}");

        var httpContent = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");
        request.Content = httpContent;

        return request;
    }

    public HttpRequestMessage MakeUpdateRequest(int id, ResourceType data)
    {
        var request = MakeBasicRequest(
            HttpMethod.Put,
            $"{Endpoint}/{id}");

        var httpContent = new StringContent(
            JsonSerializer.Serialize(data),
            Encoding.UTF8,
            "application/json");
        request.Content = httpContent;

        return request;
    }

    public HttpRequestMessage MakeDeleteRequest(int id)
        => MakeBasicRequest(
            HttpMethod.Delete,
            $"{Endpoint}/{id}");

    public HttpRequestMessage MakeActionRequest(int id, string action, object? payload = null)
    {
        var request = MakeBasicRequest(
            HttpMethod.Post,
            $"{Endpoint}/{id}/{action}");

        if (payload != null)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");
            request.Content = httpContent;
        }

        return request;
    }

    public async Task<ListType[]> SendGetAllPaginatedRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return [];

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<ListType[]>(responseStream, _options) ?? [];
    }

    public async Task<SingleType?> SendGetOneRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return default;

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<SingleType>(responseStream, _options);
    }

    public async Task<int> SendAddRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return default;

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<int>(responseStream, _options);
    }

    public async Task<bool> SendUpdateRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> SendDeleteRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return false;

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<bool>(responseStream, _options);
    }

    public async Task<bool> SendActionRequest(HttpRequestMessage request)
    {
        var response = await Client.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}