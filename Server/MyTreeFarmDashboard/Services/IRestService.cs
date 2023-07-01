using RestSharp;

namespace MyTreeFarmDashboard.Services;

public interface IRestService
{
    Task<RestRequest> MakeAuthorisedRequest(string resource, Method method = Method.Get);
    Task<RestRequest> MakeAuthorisedRequestWithJsonBody<T>(string resource, T body) where T : class;
    Task<RestResponse<T>> GetResource<T>(string resource);
    Task<RestResponse<T>> PostResource<T, TBody>(string resource, TBody body) where TBody : class;
    Task<RestResponse<T>> PutResource<T, TBody>(string resource, TBody body) where TBody : class;
    Task<RestResponse<T>> DeleteResource<T>(string resource);
}