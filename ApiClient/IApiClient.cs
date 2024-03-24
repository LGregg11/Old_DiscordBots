using System.Net.Http.Headers;

namespace ApiClient;

public interface IApiClient
{
    public void SetBaseAddress(string address);
    public void SetVersion(int majorVersion, int minorVersion);
    public void SetVersionPolicy(HttpVersionPolicy versionPolicy);
    public void SetUserAgent(ProductInfoHeaderValue userAgent);
    public void SetHost(string host);
    public void SetTimeout(TimeSpan timeout);
    public void SetAcceptRequestHeader(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue);
    public HttpResponseMessage Get(string endpoint);
}