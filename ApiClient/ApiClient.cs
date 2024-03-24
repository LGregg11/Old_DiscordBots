using System.Net.Http.Headers;

namespace ApiClient;

public class ApiClient : IApiClient
{
    protected readonly HttpClient client;

    public ApiClient()
    {
        client = new();
        SetVersion();
        SetVersionPolicy();
    }

    public void SetBaseAddress(string address)
    {
        client.BaseAddress = new Uri(address);
    }

    public void SetTimeout(TimeSpan timeout)
    {
        client.Timeout = timeout;
    }

    public void SetRefererRequestHeader(string referer)
    {
        client.DefaultRequestHeaders.Referrer = new Uri(referer);
    }

    public void SetAcceptRequestHeader(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue)
    {
        client.DefaultRequestHeaders.Accept.Add(mediaTypeWithQualityHeaderValue);
    }

    public HttpResponseMessage Get(string endpoint)
    {
        Task<HttpResponseMessage> task = client.GetAsync(endpoint);
        task.Wait();
        return task.Result;
    }

    public void SetVersion(int majorVersion = 2, int minorVersion = 0)
    {
        client.DefaultRequestVersion = new Version(majorVersion, minorVersion);
    }

    public void SetVersionPolicy(HttpVersionPolicy versionPolicy = HttpVersionPolicy.RequestVersionExact)
    {
        client.DefaultVersionPolicy = versionPolicy;
    }

    public void SetUserAgent(ProductInfoHeaderValue userAgent)
    {
        client.DefaultRequestHeaders.UserAgent.Add(userAgent);
    }

    public void SetHost(string host)
    {
        client.DefaultRequestHeaders.Host = host;
    }
}