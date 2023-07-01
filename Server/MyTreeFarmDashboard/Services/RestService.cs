using Microsoft.AspNetCore.Authentication;
using RestSharp;

namespace MyTreeFarmDashboard.Services;

public class RestService: IRestService
{
    private readonly RestClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RestService(IHttpContextAccessor httpContextAccessor, RestClient restClient)
    {
        _client = restClient;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<RestRequest> MakeAuthorisedRequest(string resource, Method method = Method.Get)
    {
        var request = new RestRequest(resource, method);
        if (_httpContextAccessor.HttpContext != null)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            request.AddHeader("authorization", "Bearer " + accessToken);
        }
        return request;
    }
    
    public async Task<RestRequest> MakeAuthorisedRequestWithJsonBody<T>(string resource, T body) where T : class
    {
        var request = new RestRequest(resource).AddJsonBody(body);
        if (_httpContextAccessor.HttpContext != null)
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            request.AddHeader("authorization", "Bearer " + accessToken);
        }
        return request;
    }

    public async Task<RestResponse<T>> GetResource<T>(string resource)
    {
        var request = await MakeAuthorisedRequest(resource);
        return await _client.ExecuteGetAsync<T>(request);
    }
    
    public async Task<RestResponse<T>> PostResource<T, TBody>(string resource, TBody body) where TBody : class
    {
        var request = await MakeAuthorisedRequestWithJsonBody(resource, body);
        return await _client.ExecutePostAsync<T>(request);
    }
    
    public async Task<RestResponse<T>> PutResource<T, TBody>(string resource, TBody body) where TBody : class
    {
        var request = await MakeAuthorisedRequestWithJsonBody(resource, body);
        return await _client.ExecutePutAsync<T>(request);
    }
    
    public async Task<RestResponse<T>> DeleteResource<T>(string resource)
    {
        var request = await MakeAuthorisedRequest(resource, Method.Delete);
        return await _client.ExecuteAsync<T>(request);
    }
}